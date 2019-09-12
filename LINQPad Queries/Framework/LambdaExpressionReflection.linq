<Query Kind="Program" />

void Main()
{
	string s = "";
	
	getPropertyInfoEx(() => s.Length).Name.Dump();
	getPropertyInfoEx(() => s.Length).PropertyType.Dump();
	
	getPropertyInfoEx(() => s.Length).Dump();
	
	(getPropertyInfoEx(() => s.Length).PropertyType == typeof(int)).Dump();	
}

static PropertyInfo getPropertyInfoEx<TProperty>(Expression<Func<TProperty>> expression)
{
  var lambda = (LambdaExpression)expression;

  MemberExpression memberExpression;
  if (lambda.Body is UnaryExpression)
  {
      var unaryExpression = (UnaryExpression)lambda.Body;
      memberExpression = (MemberExpression)unaryExpression.Operand;
  }
  else memberExpression = (MemberExpression)lambda.Body;

  return (PropertyInfo)memberExpression.Member;
}
