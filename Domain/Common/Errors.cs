namespace Domain.Common
{
    public static class Errors
    {
        public static class Student
        {
            public static Error TooManyEnrollments() =>
                new Error("student.too.many.enrollments", "Student cannot have more than 2 enrollments");

            public static Error AlreadyEnrolled(string courseName) =>
                new Error("student.already.enrolled", $"Student is already enrolled into course '{courseName}'");

            public static Error EmailIsTaken() =>
                new Error("student.email.is.taken", "Student email is taken");

            public static Error InvalidState(string name) =>
                new Error("invalid.state", $"Invalid state: '{name}'");

            public static Error CourseIsInvalid() =>
                new Error("course.is.invalid", "Course is invalid");
        }

        public static class General
        {
            public static Error NotFound(long? id = null)
            {
                string forId = id == null ? "" : $" for Id '{id}'";
                return new Error("record.not.found", $"Record not found{forId}");
            }

            public static Error ValueIsInvalid() =>
                new Error("value.is.invalid", "Value is invalid");

            public static Error ValueIsRequired() =>
                new Error("value.is.required", "Value is required");

            public static Error InvalidLength(string name)
            {
                string label = name == null ? " " : " " + name + " ";
                return new Error("invalid.string.length", $"Invalid{label}length");
            }

            public static Error CollectionIsTooSmall(int min, int current)
            {
                return new Error(
                    "collection.is.too.small",
                    $"The collection must contain {min} items or more. It contains {current} items.");
            }

            public static Error CollectionIsTooLarge(int max, int current)
            {
                return new Error(
                    "collection.is.too.large",
                    $"The collection must contain {max} items or more. It contains {current} items.");
            }

            public static Error InternalServerError(string message)
            {
                return new Error("internal.server.error", message);
            }
        }
    }
}
