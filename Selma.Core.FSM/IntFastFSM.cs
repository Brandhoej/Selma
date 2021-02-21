using Selma.Core.FSM.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Selma.Core.FSM
{
    public class IntFastFSM
        : IFSM<int, int>
    {
        public IntFastFSM(int[,] transitions, IEnumerable<int> finalStates, int initialState = 0)
        {
            if (initialState < 0 || initialState >= transitions.GetLength(0))
            {
                throw new ArgumentException($"{nameof(initialState)} is {initialState} and is outside the bounds of the {nameof(transitions)}");
            }

            CurrentState = initialState;
            Transitions = transitions;
            FinalStates = new HashSet<int>(finalStates);
        }

        public int CurrentState { get; set; }

        public int StatesCount => Transitions.GetLength(0);
        public int AlphabethCount => Transitions.GetLength(1);
        public bool IsInFinalState => FinalStates.Contains(CurrentState);

        private int[,] Transitions { get; }
        private IEnumerable<int> FinalStates { get; }

        public bool EndsInFinalState(int letter, out int finalState)
        {
            int initialState = CurrentState;
            finalState = DoTransition(letter);
            bool isInFinalState = IsInFinalState;
            CurrentState = initialState;
            return isInFinalState;
        }

        public bool EndsInFinalState(IEnumerable<int> word, out int finalState)
        {
            int initialState = CurrentState;
            finalState = DoTransition(word);
            bool isInFinalState = IsInFinalState;
            CurrentState = initialState;
            return isInFinalState;
        }

        public bool Transition(int letter, out int newState)
        {
            if (letter < 0 || letter >= AlphabethCount)
            {
                throw new ArgumentException($"'{letter}' is outside the alphabeth");
            }
            newState = DoTransition(letter);
            return IsInFinalState;
        }

        public bool Transition(IEnumerable<int> word, out int newState)
        {
            if (word == null)
            {
                throw new ArgumentNullException(nameof(word));
            }

            newState = DoTransition(word);
            return IsInFinalState;
        }

        public bool TryTransition(int letter, out int newState)
        {
            newState = CurrentState;
            if (EndsInFinalState(letter, out int finalState))
            {
                CurrentState = finalState;
                return true;
            }
            return false;
        }

        public bool TryTransition(IEnumerable<int> word, out int newState)
        {
            newState = CurrentState;
            if (EndsInFinalState(word, out int finalState))
            {
                CurrentState = finalState;
                return true;
            }
            return false;
        }

        private int DoTransition(IEnumerable<int> word)
        {
            foreach (int letter in word)
            {
                CurrentState = DoTransition(letter);
            }
            return CurrentState;
        }

        private int DoTransition(int letter)
            => CurrentState = Transitions[CurrentState, letter];
    }
}
