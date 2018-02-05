using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for EFBroker_Category
/// </summary>
/// 

//AUTHOR : TAN WEN SONG
//AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN
public class EFBroker_Category
{
    public EFBroker_Category()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static List<Category> GetCategoryList()
    {
        List<Category> categories;
        using(StationeryEntities categoryDB = new StationeryEntities()) { 
        categories = categoryDB.Categories.OrderBy(x => x.CategoryID).ToList();
        }
        return categories;
    }
    public static List<string> GetAllCategoryNames()
    {
        List<string> categoryNames;
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            categoryNames = categoryDB.Categories.Select(x => x.CategoryName).ToList();
        }
        return categoryNames;
    }
    public static Category GetCategorybyID(int categoryID)
    {
        Category cat;
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            cat = categoryDB.Categories.Where(x => x.CategoryID == categoryID).FirstOrDefault();
        }
        return cat;
    }
    public static Category GetCategorybyName(string categoryName)
    {
        Category cat;
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            cat = categoryDB.Categories.Where(x => x.CategoryName.Equals(categoryName)).FirstOrDefault();
        }
        return cat;
    }
    public static void AddCategory(Category category)
    {
        using (StationeryEntities categoryDB = new StationeryEntities())
        {
            categoryDB.Categories.Add(category);
            categoryDB.SaveChanges();
        }
        return;
    }
}