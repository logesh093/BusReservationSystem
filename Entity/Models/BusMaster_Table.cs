﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Entity.Models;

public partial class BusMaster_Table
{
    public int Bus_Id { get; set; }

    public string Bus_Name { get; set; }

    public int Window_Seats { get; set; }

    public int NonWindow_Seats { get; set; }

    public bool Is_Deleted { get; set; }

    public DateTime Update_Time_Stramp { get; set; }

    public DateTime Create_Time_Stramp { get; set; }
}