﻿//------------------------------------------------------------------------------
// <auto-generated>
//    此代码是根据模板生成的。
//
//    手动更改此文件可能会导致应用程序中发生异常行为。
//    如果重新生成代码，则将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace EohiDataServerApi.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Objects;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    
    public partial class kailifonEntities : DbContext
    {
        public kailifonEntities()
            : base("name=kailifonEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<a_systeminfo> a_systeminfo { get; set; }
        public DbSet<a_system_updatefile> a_system_updatefile { get; set; }
        public DbSet<api_items> api_items { get; set; }
        public DbSet<api_items_on> api_items_on { get; set; }
        public DbSet<api_links> api_links { get; set; }
        public DbSet<api_quartz> api_quartz { get; set; }
        public DbSet<api_webapp> api_webapp { get; set; }
        public DbSet<api_databoard> api_databoard { get; set; }
        public DbSet<api_databoard_items> api_databoard_items { get; set; }
        public DbSet<api_databoard_items_pub> api_databoard_items_pub { get; set; }
        public DbSet<api_databoard_pub> api_databoard_pub { get; set; }
        public DbSet<api_type_htmlhelp> api_type_htmlhelp { get; set; }
        public DbSet<api_menu> api_menu { get; set; }
        public DbSet<a_3d_model_files> a_3d_model_files { get; set; }
        public DbSet<a_flowchart> a_flowchart { get; set; }
        public DbSet<a_flowchart_instance_pars> a_flowchart_instance_pars { get; set; }
        public DbSet<a_flowchart_line> a_flowchart_line { get; set; }
        public DbSet<a_flowchart_node_approveitem> a_flowchart_node_approveitem { get; set; }
        public DbSet<a_flowchart_node_switchitem> a_flowchart_node_switchitem { get; set; }
        public DbSet<a_3d_models> a_3d_models { get; set; }
        public DbSet<a_3d_scene> a_3d_scene { get; set; }
        public DbSet<a_flowchart_instance> a_flowchart_instance { get; set; }
        public DbSet<api_role_menu> api_role_menu { get; set; }
        public DbSet<api_user_role> api_user_role { get; set; }
        public DbSet<a_flowchart_node> a_flowchart_node { get; set; }
        public DbSet<api_role> api_role { get; set; }
        public DbSet<api_user> api_user { get; set; }
        public DbSet<a_system_article> a_system_article { get; set; }
    
        public virtual ObjectResult<Nullable<int>> pr_app_databoard_pub(string boardno, string userid)
        {
            var boardnoParameter = boardno != null ?
                new ObjectParameter("boardno", boardno) :
                new ObjectParameter("boardno", typeof(string));
    
            var useridParameter = userid != null ?
                new ObjectParameter("userid", userid) :
                new ObjectParameter("userid", typeof(string));
    
            return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<Nullable<int>>("pr_app_databoard_pub", boardnoParameter, useridParameter);
        }
    }
}