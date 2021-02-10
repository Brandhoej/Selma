using Selma.Core.FSM;
using Selma.Core.FSM.Abstractions;
using System.Linq;

namespace Selma.Core.Domain
{
    public interface IEntityFSMBuilder
    {
        void AddTransition(int from, int letter, int to);
        void AddTransition(object from, object letter, object to);
        IFSM<int, int> Build(int initialState = 0);
    }

    public class EntityFSMBuilder
        : IEntityFSMBuilder
    {
        public EntityFSMBuilder(int statesCount, int alphabethCount)
        {
            StatesCount = statesCount;
            AlphabethCount = alphabethCount;
            TransitionTable = new int[StatesCount + 1, AlphabethCount];
            InitializeTransitionTable();
        }

        private void InitializeTransitionTable()
        {
            for (int x = 0; x < StatesCount + 1; x++)
            {
                for (int y = 0; y < AlphabethCount; y++)
                {
                    TransitionTable[x, y] = StatesCount;
                }
            }
        }

        private int StatesCount { get; }
        private int AlphabethCount { get; }
        private int[,] TransitionTable { get; set; }

        public void AddTransition(object from, object letter, object to)
            => AddTransition((int)from, (int)letter, (int)to);

        public void AddTransition(int from, int letter, int to)
        {
            TransitionTable[from, letter] = to;
        }

        public IFSM<int, int> Build(int initialState = 0)
            => new IntFastFSM(TransitionTable, Enumerable.Range(0, StatesCount), initialState);
    }
}
