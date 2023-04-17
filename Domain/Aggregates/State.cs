﻿namespace Domain.Aggregates
{
    public class State : ValueObject
    {
        public string Value { get; }

        private State(string value)
        {
            Value = value;
        }

        public static Result<State, Error> Create(string input, string[] allStates)
        {
            if (string.IsNullOrWhiteSpace(input))
                return Errors.General.ValueIsRequired();

            string name = input.Trim().ToUpper();

            if (name.Length > 2)
                return Errors.General.InvalidLength();

            if (allStates.Any(x => x == name) == false)
                return Errors.Student.InvalidState(name);

            return new State(name);
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
