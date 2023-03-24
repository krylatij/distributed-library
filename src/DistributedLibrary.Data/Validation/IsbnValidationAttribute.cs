using System.ComponentModel.DataAnnotations;


namespace DistributedLibrary.Data.Validation
{
    public class IsbnValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return default;
            }

            if (!(value is string))
            {
                return new ValidationResult("ISBN should be a string.");
            }

            var isbn = value as string;

            var errorMsg = new ValidationResult("ISBN should be provided in either ISBN-10 or ISBN-13 format.");

            var isbnNormalized = isbn.Replace("-", "");

            if (isbnNormalized.Length == 10)
            {
                var sum = 0;
                for (int i = 0; i < isbnNormalized.Length; i++)
                {
                    var d = isbnNormalized[i] - 48;
                    if (d < 0 || d > 9)
                    {
                        return errorMsg;
                    }

                    sum += d * (10 - i);
                }

                return sum % 11 == 0 ? null : errorMsg;
            }

            if (isbnNormalized.Length == 13)
            {
                var nums = new int[13];

                for (int i = 0; i < isbnNormalized.Length; i++)
                {
                    var d = isbnNormalized[i] - 48;
                    if (d < 0 || d > 9)
                    {
                        return errorMsg;
                    }

                    nums[i] = d;
                }

                var sum = nums[0] + nums[1] * 3 + nums[2] + nums[3] * 3 + nums[4] + nums[5] * 3 + nums[6] +
                          nums[7] * 3 + nums[8] + nums[9] * 3 + nums[10] + nums[11] * 3 + nums[12];

                return sum % 10 == 0 ? null : errorMsg;

            }

            return errorMsg;
        }
    }
}
