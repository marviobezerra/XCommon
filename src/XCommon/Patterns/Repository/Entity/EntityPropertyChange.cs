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
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(caller));
		}

		protected void RaisePropertyChanged<TEntity>(Expression<Func<TEntity>> memberExpression)
		{
			if (memberExpression.Body is MemberExpression body)
			{
				RaisePropertyChanged(body.Member.Name);
			}

			throw new ArgumentException("Lambda must return a property.");
		}

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
