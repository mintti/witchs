using System.Collections;
using System.Collections.Generic;

namespace Infra
{
    public interface IBattleState
    {
        int hp{get;set;}
        int power{get;set;}
    }

    public interface IMonster : IBattleState
    {
        int num {get; set;}
        string name{get;set;}
        
        void Attack();       
    }

    public interface INPC
    {
        int num{get;set;}
        string name{get;set;}
        int favor{get;set;}
        int interest{get;set;}
        int scenario{get;set;}

    }

    public interface IMedicine
    {
        List<int> material{get;set;}
        float probability{get;set;}
    }

    public interface ICultivation //Grow
    {
        string name{get;set;}
    }
    
}