﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class UserOrAdminSignup_Table
{
    public int UserId { get; set; }

    public string Name { get; set; }

    public string Gender { get; set; }

    public string DOB { get; set; }

    public string EmailId { get; set; }

    public string Address { get; set; }

    public string Phonenumber { get; set; }

    public string Hash_Password { get; set; }

    public byte[] Salt_Password { get; set; }

    public bool Is_Admin { get; set; }
}