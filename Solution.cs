using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

public interface IRealEstateListing
{
  int ID { get; set; }
  string Title { get; set; }
  string Description { get; set; }
  int Price { get; set; }
  string Location { get; set; }
}

public interface IRealEstateApp
{
  void AddListing(IRealEstateListing listing);
  void RemoveListing(int listingID);
  void UpdateListing(IRealEstateListing listing);
  List<IRealEstateListing> GetListings();
  List<IRealEstateListing> GetListingsByLocation(string location);
  List<IRealEstateListing> GetListingsByPriceRange(int minPrice, int maxPrice);
}

class RealEstateListing : IRealEstateListing
{
  public int ID { get; set; }
  public string Title { get; set; }
  public string Description { get; set; }
  public int Price { get; set; }
  public string Location { get; set; }
}

class RealEstateApp : IRealEstateApp
{
  private List<IRealEstateListing> listings = new List<IRealEstateListing>();

  public void AddListing(IRealEstateListing listing)
  {
    if (listing != null)
    {
      listings.Add(listing);
    }
    else
    {
      throw new ArgumentNullException(nameof(listing), "Listing cannot be null");
    }
  }

  public void RemoveListing(int listingId)
  {
    listings.RemoveAll(listing => listing.ID == listingId);
  }

  public void UpdateListing(IRealEstateListing updateListing)
  {
    if (updateListing == null)
      throw new ArgumentNullException(nameof(updateListing), "Listing cannot be null");

    var existingListing = listings.Find(listing => listing.ID == updateListing.ID);

    if (existingListing != null)
    {
      existingListing.Title = updateListing.Title;
      existingListing.Description = updateListing.Description;
      existingListing.Price = updateListing.Price;
      existingListing.Location = updateListing.Location;
    }
    else
    {
      throw new ArgumentException("Listing cannot be found");
    }
  }

  public IReadOnlyList<IRealEstateListing> GetListings()
  {
    return listings.AsReadOnly();
  }

  public List<IRealEstateListing> GetListingsByLocation(string location)
  {
    if (string.IsNullOrWhiteSpace(location))
    {
      throw new ArgumentException("Location cannot be null");
    }

    return listings
        .Where(listing => listing
                .Location.Equals(location, StringComparison.OrdinalIgnoreCase))
                .ToList();
  }

  public List<IRealEstateListing> GetRealEstateListingsByPriceRange(int minPrice, int maxPrice)
  {
    if (minPrice < 0 || maxPrice < 0 || minPrice > maxPrice)
    {
      throw new ArgumentException("Invalid range price");
    }

    return listings.Where(listing => listing.Price >= minPrice && listing.Price <= maxPrice).ToList();
  }

    List<IRealEstateListing> IRealEstateApp.GetListings()
    {
        throw new NotImplementedException();
    }

    public List<IRealEstateListing> GetListingsByPriceRange(int minPrice, int maxPrice)
    {
        throw new NotImplementedException();
    }
}

class Solution
{
  public static void Main(string[] args)
  {
    TextWriter textWriter = new StreamWriter(System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);
    IRealEstateApp app = new RealEstateApp();
    int lCount = Convert.ToInt32(Console.ReadLine().Trim());
    for (int i = 1; i <= lCount; i++)
    {
      var a = Console.ReadLine().Trim().Split(" ");
      IRealEstateListing e = new RealEstateListing();
      e.ID = Convert.ToInt32(a[0]);
      e.Title = a[1];
      e.Description = a[2];
      e.Price = Convert.ToInt32(a[3]);
      e.Location = a[4];
      app.AddListing(e);
    }

    textWriter.WriteLine("All Listings:");
    List<IRealEstateListing> allListings = app.GetListings();
    foreach (var listing in allListings)
    {
      textWriter.WriteLine($"ID: {listing.ID}, Title: {listing.Title}, Price: {listing.Price} , Location: {listing.Location}");
    }

    var b = Console.ReadLine().Trim().Split(" ");
    var location = b[0];
    textWriter.WriteLine($"Listings in {location}:");
    List<IRealEstateListing> listingsByLocation = app.GetListingsByLocation(location);
    foreach (var listing in listingsByLocation)
    {
      textWriter.WriteLine($"ID: {listing.ID}, Title: {listing.Title}, Price: {listing.Price}");
    }
    var c = Console.ReadLine().Trim().Split(" ");
    var minPrice = Convert.ToInt32(c[0]);
    var maxPrice = Convert.ToInt32(c[1]);
    var getListingsByPriceRange = app.GetListingsByPriceRange(minPrice, maxPrice);
    textWriter.WriteLine($"Listings By Price Range ({minPrice} - {maxPrice}):");
    foreach (var item in getListingsByPriceRange)
    {
      textWriter.WriteLine($"ID: {item.ID}, Title: {item.Title}, Price: {item.Price}");
    }



    textWriter.Flush();
    textWriter.Close();
  }
}
