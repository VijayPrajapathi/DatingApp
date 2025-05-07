using System;

namespace API.Entities;

public static class DateTimeExtensions
{
 public static int  CalculateAge(this DateTime dob){
    var today = DateOnly.FromDateTime(DateTime.Now);
    var age = today.Year - dob.Year;
        if (DateOnly.FromDateTime(dob) > today.AddYears(-age)) age--;
    return age;
 }
}
