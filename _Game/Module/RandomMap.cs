using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace RandomMapMaker
{
    public enum Cardinal
    { 
        none,
        north,
        west,
        east,
        south
    }
    

    public struct Vector2
    {
        private int x;
        private int y;
        public int X { get { return x; } }
        public int Y { get { return y; } }

        public Vector2(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public Vector2(int[] arr)
        {
            this.x = arr[0];
            this.y = arr[1];
        }
        public int[] GetArray()
        {
            return new int[2] { x, y };
        }
        
    }


    public class Map
    {
        public int? num;
        Vector2 pivot; //array[p , q]
        public Vector2 Pivot { get { return pivot; } set { pivot = value; } }
        private Dictionary<Cardinal, Map> root;
        public Dictionary<Cardinal, Map> Root { get { return root; } }
        public Map parent;
        public Cardinal parentCardinal;

        public Map() { }
        public Map(int num){ this.num = num; root = new Dictionary<Cardinal, Map>(); }
        public Map(Vector2 pivot, int num)
        {
            this.pivot = pivot;
            this.num = num;

            parentCardinal = Cardinal.none;
            root = new Dictionary<Cardinal, Map>();
        }
      
        public void ConnectMap(Map map, Cardinal cardinal)
        {
            root.Add(cardinal, map);
        }

        public void SetParent(Map parentMap, Cardinal card, Vector2 pivot)
        {
            this.parent = parentMap;
            this.parentCardinal = card;
            this.pivot = pivot;
        }
    }

    /* -------------------------------
      
             호출은 MapMaker() 
     
     ------------------------------= */
    public class Program
    {
        static int?[,] arr = null;
        static List<Cardinal> cardinalList; // Cardinal List
        static Dictionary<Cardinal, Vector2> cadinalVec; //Cardinal별 좌표
        static Dictionary<Cardinal, Cardinal> cadinalCounter; //상대성 부모위치 예외
        static Dictionary<Map, List<Cardinal>> exceptionDic; //예외사전
        static Random random;


        // static void Main(string[] args)
        // {
            
        //     Map map = Program.MapMaker(20, 50);

        //     PrintArray();

        //     return;
        // }


        static private void SetData()
        {
            cadinalVec = new Dictionary<Cardinal, Vector2>()
            { { Cardinal.north, new Vector2(-1, 0) },
              { Cardinal.east, new Vector2(0, 1) },
              { Cardinal.west, new Vector2(0, -1) },
              { Cardinal.south, new Vector2(1, 0) } };

            cadinalCounter = new Dictionary<Cardinal, Cardinal>()
            { { Cardinal.north, Cardinal.south },
              { Cardinal.east, Cardinal.west },
              { Cardinal.west, Cardinal.east },
              { Cardinal.south, Cardinal.north } };

            cardinalList = new List<Cardinal>() { Cardinal.north, Cardinal.east, Cardinal.west, Cardinal.south };

            exceptionDic = new Dictionary<Map, List<Cardinal>>();

            random = new Random();
        }


        //size 는 정사각형 arr의 Length, count는 채울 숫자
        public static Map MapMaker(int size, int count)
        {
            Program.SetData();

            arr = new int?[size, size];

            Vector2 mainVec = new Vector2(size/2, size/2);
            Map mainMap = new Map(mainVec, 1);
            arr[mainVec.X, mainVec.Y] = 1;


            for (int i = 2; i <= count; i++)
            {
                Map newMap = new Map(i);
                newMap = Connect(newMap, mainMap);
            }
            return mainMap;
        }


        //Parent에 적절한 위치(random)에 Map을 넣어줌
        static Map Connect(Map map, Map parent)
        {
            Cardinal randomCardinal;

            randomCardinal = MapRootCheck(parent);

            //방향이 존재하지 않을 경우 => 부모의 부모에게 현제 들어온 위치를 예외처리 
            if (randomCardinal == Cardinal.none)
            {
                 AddExceptionDictionary(parent.parent, parent.parent.Root.FirstOrDefault(c => c.Value == parent).Key);
                    return Connect(map, parent.parent);
            }

            int x = parent.Pivot.X + cadinalVec[randomCardinal].X;
            int y = parent.Pivot.Y + cadinalVec[randomCardinal].Y;

            //Root 부적합. 해당 방향에 존재시 존재하는 Map을 부모로 재연결시도
            if (arr[x, y] != null)
            {
                //해당 위치에 부모의 자식이 아닌 다른 가지의 자식이 존재?
                if (!(parent.Root.ContainsKey(randomCardinal))) 
                {
                    AddExceptionDictionary(parent, randomCardinal);
                    return Connect(map, parent);
                }
                
                return Connect(map, parent.Root[randomCardinal]); //재연결
            }


            //두 조건 충족. 부모 연결. 부모에게 자식연결
            map.SetParent(parent, cadinalCounter[randomCardinal], new Vector2(x, y));
            parent.ConnectMap(map, randomCardinal);
            AddExceptionDictionary(map, cadinalCounter[randomCardinal]); //부모위치 삭제
            arr[x, y] = map.num;

            return map;
        }


        //유효한 위치지정해주는 함수
        static Cardinal MapRootCheck(Map map)
        { 
            List<Cardinal> exception = new List<Cardinal>( exceptionDic.ContainsKey(map) ? exceptionDic[map] : new List<Cardinal>());
  
            if (exception.Count == 4) return Cardinal.none; //탈출시도1

            List<Cardinal> list = new List<Cardinal>(cardinalList);
            
            //예외위치 삭제
            foreach (Cardinal c in exception)
                list.Remove(c);
            
            //Arr를 벗어나지 않는 값이어야함. 벗어나는 값은 다른 List에 추가 후 삭제
            List<Cardinal> removelist = new List<Cardinal>();
            foreach (Cardinal c in list)
            {
                int x = map.Pivot.X + cadinalVec[c].X;
                int y = map.Pivot.Y + cadinalVec[c].Y;

                if (x < 0 || x >= arr.GetLength(0) || y < 0 || y >= arr.GetLength(1))
                {
                    removelist.Add(c);
                    AddExceptionDictionary(map, c);
                }
            }
            while (removelist.Count != 0)
            {
                list.Remove(removelist[0]);
                removelist.RemoveAt(0);
            }

            //방향 전부 오류일때. 탈출시도2
            if (list.Count == 0)
                return Cardinal.none;

            //남은 방향중에서 지정해줌
            int index = random.Next(list.Count);
            return list[index];
        }


        //예외사전에 중복없이 값등록
        static void AddExceptionDictionary(Map map, Cardinal c)
        {
            if (exceptionDic.ContainsKey(map))
            {
                if (exceptionDic[map].Contains(c) == false)
                    exceptionDic[map].Add(c);
                return;
            }
            else exceptionDic.Add(map, new List<Cardinal>() { c });
        }


        #region 출력관련
        //Array형태로 출력
        static private void PrintArray()
        {
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    Console.Write("{0, 5}", arr[i, j] == null ? " " : arr[i, j].ToString());
                }
                Console.WriteLine();
            }
        }
        
        static private void PrintTree()
        {
            /*
            bool dont;
            int depth = 0;
            do
            {
                dont = false;
                dont = dont ? true : FindDepth(map, 0, depth);
                depth++;
            } while (dont);
            */
        }
        #endregion

        static bool FindDepth(Map map, int depth, int targetDepth)
        {
            if (depth > targetDepth)
                return false;

            else if (depth == targetDepth)
            {
                return true;
            }

            return FindDepth(map, ++depth, targetDepth);
        }

        static int GetDepth(Map map, int depth)
        {
            
            return depth;
        }
    }
}