using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace EohiDataServerApi
{
    /// <summary>
    /// mssql数据库 数据层 父类
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFDBHelper<T> : EFDBHelperBase<T> where T : class,new()
    {
        #region INSERT

        /// <summary>
        /// 新增 实体
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Insert(T model)
        {
            Db.Set<T>().Add(model);
        }

        /// <summary>
        /// 普通批量插入
        /// </summary>
        /// <param name="datas"></param>
        //public void InsertRange(List<T> datas)
        //{
        //    Db.Set<T>().AddRange(datas);
        //}

        #endregion INSERT

        #region DELETE

        /// <summary>
        /// 根据模型删除
        /// </summary>
        /// <param name="model">包含要删除id的对象</param>
        /// <returns></returns>
        public void Delete(T model)
        {
            Db.Set<T>().Attach(model);
            Db.Set<T>().Remove(model);
        }

        /// <summary>
        /// 根据条件删除
        /// </summary>
        /// <param name="whereLambda"></param>
        public void Delete(Expression<Func<T, bool>> whereLambda)
        {
          List<T>list=  Db.Set<T>().Where(whereLambda).ToList();
            list.ForEach(u =>
            {
                Delete(u);
            });
        }


        #endregion DELETE

        #region UPDATE

        /// <summary>
        /// 单个对象指定列修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <param name="isProUpdate"></param>
        /// <returns></returns>
        public void Update(T model, List<string> proNames, bool isProUpdate = true)
        {
            //将 对象 添加到 EF中
            Db.Set<T>().Attach(model);
            var setEntry = ((IObjectContextAdapter)Db).ObjectContext.ObjectStateManager.GetObjectStateEntry(model);
            //指定列修改
            if (isProUpdate)
            {
                foreach (string proName in proNames)
                {
                    setEntry.SetModifiedProperty(proName);
                }
            }
            //忽略类修改
            else
            {
                Type t = typeof(T);
                List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
                foreach (var item in proInfos)
                {
                    string proName = item.Name;
                    if (proNames.Contains(proName))
                    {
                        continue;
                    }
                    setEntry.SetModifiedProperty(proName);
                }
            }
        }

        /// <summary>
        /// 单个对象修改
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public void Update(T model)
        {
            DbEntityEntry entry = Db.Entry<T>(model);
            Db.Set<T>().Attach(model);
            entry.State = EntityState.Modified;

        }

 
   

        /// <summary>
        /// 批量修改，多参数
        /// T u = new T() { uId = 1, uLoginName = "asdfasdf" };
        /// this.Modify(u, "uLoginName");
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="proNames">要修改的 属性 名称</param>
        /// <returns></returns>
        public void Update(T model, params string[] proNames)
        {
            //4.1将 对象 添加到 EF中
            DbEntityEntry entry = Db.Entry<T>(model);
            //4.2先设置 对象的包装 状态为 Unchanged
            entry.State =EntityState.Unchanged;
            //4.3循环 被修改的属性名 数组
            foreach (string proName in proNames)
            {
                //4.4将每个 被修改的属性的状态 设置为已修改状态;后面生成update语句时，就只为已修改的属性 更新
                entry.Property(proName).IsModified = true;
            }
         
        }

        /// <summary>
        /// 批量修改
        /// </summary>
        /// <param name="models"></param>
        /// <returns></returns>
        public void UpdateAll(List<T> models)
        {
            foreach (var model in models)
            {
                DbEntityEntry entry = Db.Entry(model);
                entry.State = EntityState.Modified;
            }


        }


        /// <summary>
        /// 批量统一修改
        /// </summary>
        /// <param name="model">要修改的实体对象</param>
        /// <param name="whereLambda">查询条件</param>
        /// <param name="modifiedProNames">修改的参数</param>
        /// <returns></returns>
        public void Update(T model, Expression<Func<T, bool>> whereLambda, params string[] modifiedProNames)
        {
            //查询要修改的数据
            List<T> listModifing = Db.Set<T>().Where(whereLambda).ToList();
            Type t = typeof(T);
            List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            Dictionary<string, PropertyInfo> dictPros = new Dictionary<string, PropertyInfo>();
            proInfos.ForEach(p =>
            {
                if (modifiedProNames.Contains(p.Name))
                {
                    dictPros.Add(p.Name, p);
                }
            });
            if (dictPros.Count <= 0)
            {
                throw new Exception("指定修改的字段名称有误或为空");
            }
            foreach (var item in dictPros)
            {
                PropertyInfo proInfo = item.Value;

                //取出 要修改的值
                object newValue = proInfo.GetValue(model, null);

                //批量设置 要修改 对象的 属性
                foreach (T oModel in listModifing)
                {
                    //为 要修改的对象 的 要修改的属性 设置新的值
                    proInfo.SetValue(oModel, newValue, null);
                }
            }

        }

        #endregion UPDATE

        #region SELECT

        /// <summary>
        /// 根据主键查询
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindById(dynamic id)
        {
            return Db.Set<T>().Find(id);
        }

        /// <summary>
        /// 获取默认一条数据，没有则为NULL
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public T FirstOrDefault(Expression<Func<T, bool>> whereLambda = null)
        {
            if (whereLambda == null)
            {
                return Db.Set<T>().FirstOrDefault();
            }
            return Db.Set<T>().FirstOrDefault(whereLambda);
        }

        /// <summary>
        /// 获取全部数据
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll<Tkey>(Func<T,Tkey> orderingLamuda = null)
        {
            return orderingLamuda==null?
                Db.Set<T>().ToList()
                : Db.Set<T>().ToList().OrderBy<T, Tkey>(orderingLamuda).ToList();
        }
        /// <summary>
        /// 获取全部数据降序
        /// </summary>
        /// <returns></returns>
        public List<T> GetAllDes<Tkey>(Func<T, Tkey> orderingLamuda = null)
        {
            return orderingLamuda == null ?
                Db.Set<T>().ToList()
                : Db.Set<T>().ToList().OrderByDescending<T, Tkey>(orderingLamuda).ToList();
        }

        /// <summary>
        /// 带条件查询获取数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <returns></returns>
        public List<T> GetAll<Tkey>(Expression<Func<T, bool>> whereLambda, Func<T, Tkey> orderbyLambda)
        {
            var iQueryable = Db.Set<T>().Where(whereLambda);
            return orderbyLambda == null
                ? iQueryable.ToList()
                : iQueryable.OrderBy<T, Tkey>(orderbyLambda).ToList();
        }


        /// <summary>
        /// 带条件查询获取数据
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public List<T> GetList<Tkey>(Expression<Func<T, bool>> whereLambda)
        {
            var iQueryable = Db.Set<T>().Where(whereLambda);
            return  iQueryable.ToList();
        }

        /// <summary>
        /// 带条件查询获取数据/降序
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <param name="orderbyLambda"></param>
        /// <returns></returns>
        public List<T> GetAllDes<Tkey>(Expression<Func<T, bool>> whereLambda, Func<T, Tkey> orderbyLambda)
        {
            var iQueryable = Db.Set<T>().Where(whereLambda);
            return orderbyLambda == null
                ? iQueryable.ToList()
                : iQueryable.OrderByDescending<T, Tkey>(orderbyLambda).ToList();
        }
        /// <summary>
        /// 带条件查询获取数据/末tolist
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>
        public IQueryable<T> GetAllIQueryable(Expression<Func<T, bool>> whereLambda = null)
        {
            return whereLambda == null ? Db.Set<T>() : Db.Set<T>().Where(whereLambda);
        }

        /// <summary>
        /// 获取数量
        /// </summary>
        /// <param name="whereLambd"></param>
        /// <returns></returns>
        public int GetCount(Expression<Func<T, bool>> whereLambd = null)
        {
            return whereLambd == null ? Db.Set<T>().Count() : Db.Set<T>().Where(whereLambd).Count();
        }

        /// <summary>
        /// 判断对象是否存在
        /// </summary>
        /// <param name="whereLambd"></param>
        /// <returns></returns>
        public bool Any(Expression<Func<T, bool>> whereLambd)
        {
            return Db.Set<T>().Where(whereLambd).Any();
        }

        /// <summary>  
        /// 分页查询 + 条件查询 + 排序  
        /// </summary>  
        /// <typeparam name="Tkey">泛型</typeparam>  
        /// <param name="pageSize">每页大小</param>  
        /// <param name="pageIndex">当前页码</param>  
        /// <param name="total">总数量</param>  
        /// <param name="whereLambda">查询条件</param>  
        /// <param name="orderbyLambda">排序条件</param>  
        /// <param name="isAsc">是否升序</param>  
        /// <returns>IQueryable 泛型集合</returns>  
        public IQueryable<T> LoadPageItems<Tkey>(int pageSize, int pageIndex, out int total, Expression<Func<T, bool>> whereLambda, Func<T, Tkey> orderbyLambda, bool isAsc)
        {
            total = Db.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = Db.Set<T>().Where(whereLambda)
                             .OrderBy<T, Tkey>(orderbyLambda)
                             .Skip(pageSize * (pageIndex - 1))
                             .Take(pageSize);
                return temp.AsQueryable();
            }
            else
            {
                var temp = Db.Set<T>().Where(whereLambda)
                           .OrderByDescending<T, Tkey>(orderbyLambda)
                           .Skip(pageSize * (pageIndex - 1))
                           .Take(pageSize);
                return temp.AsQueryable();
            }
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex">当前页码</param>
        /// <param name="pageSize">每页大小</param>
        /// <param name="rows">总条数</param>
        /// <param name="ordering">排序条件（一定要有）</param>
        /// <param name="whereLambda">查询添加（可有，可无）</param>
        /// <returns></returns>
        public List<T> Page<Tkey>(int pageIndex, int pageSize, out int rows, Func<T, Tkey> orderbyLambda, Expression<Func<T, bool>> whereLambda = null)
        {
            // 分页 一定注意： Skip 之前一定要 OrderBy
            var data = Db.Set<T>().OrderBy<T,Tkey>(orderbyLambda).AsQueryable();
            if (whereLambda != null)
            {
                data = data.Where(whereLambda);
            }
            rows = data.Count();
            return data.Skip(pageSize * (pageIndex - 1)).Take(pageSize).ToList();
        }

        ///// <summary>
        ///// 查询转换
        ///// </summary>
        ///// <typeparam name="TDto"></typeparam>
        ///// <param name="whereLambda"></param>
        ///// <returns></returns>
        //public List<TDto> Select<TDto>(Expression<Func<T, bool>> whereLambda)
        //{
        //    return Db.Set<T>().Where(whereLambda).SelectMany<TDto>().ToList();
        //}

        #endregion SELECT

        #region ORTHER

        ///// <summary>
        ///// 执行存储过程或自定义sql语句--返回集合
        ///// </summary>
        ///// <param name="sql"></param>
        ///// <param name="parms"></param>
        ///// <param name="cmdType"></param>
        ///// <returns></returns>
        //public List<T> Query(string sql, List<SqlParameter> parms, CommandType cmdType = CommandType.Text)
        //{
        //    return DbQuery<T>(sql, parms, cmdType);
        //}

        /// <summary>
        /// 提交保存
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return Db.SaveChanges();
        }

        /// <summary>
        /// 回滚
        /// </summary>
        public void RollBackChanges()
        {
            var items = Db.ChangeTracker.Entries().ToList();
            items.ForEach(o => o.State = EntityState.Unchanged);
        }

        #endregion ORTHER

    }
}
