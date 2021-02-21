using System.Collections.Generic;

namespace Selma.Core.FSM.Abstractions
{
    public interface IFSM<TAlphabet, TState>
    {
        public int CurrentState { get; set; }
        public int StatesCount { get; }
        public int AlphabethCount { get; }
        public bool IsInFinalState { get; }

        bool EndsInFinalState(TAlphabet letter, out int newState);
        bool EndsInFinalState(IEnumerable<TAlphabet> word, out int newState);

        bool Transition(TAlphabet letter, out TState newState);
        bool Transition(IEnumerable<TAlphabet> word, out TState newState);

        bool TryTransition(TAlphabet letter, out TState newState);
        bool TryTransition(IEnumerable<TAlphabet> word, out TState newState);
    }
}
