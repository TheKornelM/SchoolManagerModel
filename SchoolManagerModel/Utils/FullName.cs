using System.Globalization;

namespace SchoolManagerModel.Utils;

internal static class FullName
{
    /// <summary>
    /// Returns the full name based on the current culture.
    /// </summary>
    /// <param name="firstName">The first name of the individual.</param>
    /// <param name="lastName">The last name of the individual.</param>
    /// <returns>A string representing the full name formatted for the current culture.</returns>
    public static string Get(string firstName, string lastName)
    {
        if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName))
            throw new ArgumentException("Both first name and last name must be provided.");

        var currentCulture = CultureInfo.CurrentCulture.Name;

        // Hungarian culture formats as "LastName FirstName"
        if (currentCulture == "hu-HU")
        {
            return $"{lastName} {firstName}";
        }

        // Default to "FirstName LastName" for other cultures, including English
        return $"{firstName} {lastName}";
    }
}
