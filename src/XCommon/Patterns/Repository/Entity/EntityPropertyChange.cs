using System;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace XCommon.Patterns.Repository.Entity
{
	public abstract class EntityPropertyChange : INotifyPropertyChanged
    {
        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
        }

        protected void RaisePropertyChanged<TEntity>(Expression<Func<TEntity>> memberExpression)
        {
            if (memberExpression == null)
            {
                throw new ArgumentNullException("memberExpression");
            }

            var body = memberExpression.Body as MemberExpression;

            if (body == null)
            {
                throw new ArgumentException("Lambda must return a property.");
            }

            RaisePropertyChanged(body.Member.Name);
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
