using System.ComponentModel;
using System.Reflection;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace MGPK;

public class ArrayModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext.ModelMetadata.IsEnumerableType is false)
        {
            bindingContext.Result = ModelBindingResult.Failed();
            return Task.CompletedTask;
        }

        var inputValue = bindingContext.ValueProvider
            .GetValue(bindingContext.ModelName).ToString();
        if (string.IsNullOrEmpty(inputValue))
        {
            bindingContext.Result = ModelBindingResult.Success(null);
            return Task.CompletedTask;
        }

        var type = bindingContext.ModelType.GetTypeInfo().GenericTypeArguments[0];
        var converter = TypeDescriptor.GetConverter(type);

        var outputArray = inputValue.Split(',', StringSplitOptions.TrimEntries)
            .Select(val => converter.ConvertFromString(val))
            .ToArray();

        var typedArray = Array.CreateInstance(type, outputArray.Length);
        outputArray.CopyTo(typedArray,0);
        bindingContext.Model = outputArray;
        
        bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
        return Task.CompletedTask;
    }
}