using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for Message
/// </summary>
/// 
//AUTHOR : ARIZ ARMAND BIN ABDUL RAHMAN
public class Message
{
    public const String EmptySupplierCode
    = "Supplier Code cannot be empty.  Please enter a Supplier Code.";

    public const String EmptySupplierName
    = "Supplier Name cannot be empty.  Please enter a Supplier Name.";

    public const String EmptyContactName
    = "Contact Name cannot be empty.  Please enter a Contact Name.";

    public const String PhoneNo
    = "Phone No cannot be empty.  Please enter a Phone Number.";

    public const String FaxNo
    = "Fax No cannot be empty.  Please enter a Fax Number.";

    public const String Address
        = "Invalid Address.  Please enter a valid address.";

    public const String AddSupplierSuccessful
        = "Supplier record successfully created!";

    public const String OneItemPerSupplier
        = "Maximum 1 item per supplier in a year!";

    public const String InvalidEntry
        = "Please input valid Category & Item!";

    public const String PageInvalidEntry
        = "Please go through the proper page!";

    public const String SuccessfulItemAdd
        = "Item Added Successfully!";

    public const String UpdateSuccessful
        = "Update Successful!";

    public const String InactiveSuccessful
        = "Supplier successfuly set to Inactive!";

    public const String SupplierSuccessfulAdd
        = "Supplier Added Successfully!";

    public const String DeleteSuccessful
        = "Deleted Successfully!";

    public const String CategoryAlreadyInList
        = "Category is already in list!";

    public const String DepartmentAlreadyInList
        = "Department is already in list!";

    public const String SupplierAlreadyInList
        = "Supplier is already in list!";

    public const String DateAlreadyInList
    = "Month already selected!";

    public const String CustomFieldsNotSelected
        = "Please ensure to click 'Add' afterwards if you are selecting customised data!";

    public const String ROReport
        = "Reorder Trend Report";

    public const String RTReport
        = "Requisition Trend Report";

    public const String ValidationError
    = "One or more fields are incorrect. Please try again";

    public const String ActiveSuccessful
    = "Supplier successfully set to Active";

    public const String PriceListExistsForGivenTenderYear
    = "PriceList already exists for Item, Rank and Tender Year selected";

    public const String GeneralError
    = "Error encountered. Please try again. If issue persists, contact your local IT helpdesk for support.";

}