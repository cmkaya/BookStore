namespace Business.Constants;

public static class Messages
{
    public static string AuthorAdded = "The author has been successfully created!";
    public static string BookAdded = "The book has been successfully created!";
    public static string UserAdded = "The user has been successfully created!";
    public static string CityAdded = "The city has been successfully created!";
    public static string CountryAdded = "The country has been successfully created!";
    public static string OrderAdded = "The order has been successfully created!";
    
    public static string AuthorUpdated = "The author has been successfully updated!";
    public static string BookUpdated = "The book has been successfully updated!";
    public static string UserUpdated = "The author has been successfully updated!";
    public static string CityUpdated = "The city has been successfully updated!";
    public static string CountryUpdated = "The country has been successfully updated!";
    
    public static string AuthorDeleted = "The author has been successfully deleted!";
    public static string BookDeleted = "The book has been successfully deleted!";
    public static string BookRemovedFromCart = "The book has been successfully deleted from the cart!";
    public static string UserDeleted = "The author has been successfully updated!";
    public static string CityDeleted = "The city has been successfully deleted!";
    public static string CountryDeleted = "The country has been successfully deleted!";
    
    public static string ExistedAuthor = "The author has already existed!";
    public static string ExistedBook = "The book has already existed!";
    public static string ExistedUser = "The user name has already existed!";
    public static string ExistedEmail = "The e-mail has already existed!";
    public static string ExistedCity = "The city has already existed!";
    public static string ExistedCountry = "The country has already existed!";
    
    public static string InvalidAuthorRemove = "The author can't be deleted because it includes books.";
    public static string InvalidCityRemove = "The city can't be deleted because it includes users.";
    public static string InvalidCountryRemove = "The country cannot be deleted because it includes cities.";
    public static string InvalidCountryRemove2 = "The country cannot be deleted because it includes users.";
    public static string InvalidUserLoginInfo = "The user name or password is incorrect!";
    
    public static string NoUserFound = "No user found!";
    public static string NoCityFound = "No city found!";
    public static string NoCountryFound = "No country found!";

    public static string NoStock = "The book has no stock!";
    public static string InsufficientStock = "The stock amount of the book is insufficient!";
    public static string CartCleared = "Shopping cart has successfully cleared!";
}