﻿

//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Web_Gio_Cha.EF
{

using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;


public partial class GioChaEntities : DbContext
{
    public GioChaEntities()
        : base("name=GioChaEntities")
    {

    }

    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }


    public virtual DbSet<Menu> Menu { get; set; }

    public virtual DbSet<Order> Order { get; set; }

    public virtual DbSet<OrderDetail> OrderDetail { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<Product_Category> Product_Category { get; set; }

    public virtual DbSet<Slide> Slide { get; set; }

    public virtual DbSet<TblAddressUser> TblAddressUser { get; set; }

    public virtual DbSet<TblCity> TblCity { get; set; }

    public virtual DbSet<TblMenuContent> TblMenuContent { get; set; }

    public virtual DbSet<TblNews> TblNews { get; set; }

    public virtual DbSet<TblUser> TblUser { get; set; }

}

}

