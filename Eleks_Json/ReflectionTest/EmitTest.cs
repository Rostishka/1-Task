using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace ReflectionTest
{
    class EmitTest
    {
        public static void Run()
        {
            Emitter.Run( "Hello world!!!" );
           
        }
        public class Emitter
        {
            public static void Run(string toEmit)
            {
                Emitter emitter = new Emitter(toEmit);
                emitter.Emit();
            }

            private string toEmit;
            private AssemblyName assemblyName;
            private AssemblyBuilder assemblyBuilder;
            private ModuleBuilder moduleBuilder;
            private TypeBuilder typeBuilder;
            private MethodBuilder methodBuilder;

            protected Emitter(string toEmit)
            {
                this.toEmit = toEmit;
            }

            public void Emit()
            {
                const string name = "Hello.exe";
                assemblyName = new AssemblyName();
                assemblyName.Name = "Hello";

                // Define dynamic assembly
                assemblyBuilder = CreateAssemblyBuilder(assemblyName,
                AssemblyBuilderAccess.Save);

                // Define dynamic module
                moduleBuilder = CreateModuleBuilder(assemblyBuilder);

                // Define dynamic type
                typeBuilder = CreateTypeBuilder(moduleBuilder);

                // Define dynamic method
                methodBuilder = CreateMethodBuilder(typeBuilder);

                // Apply attributes
                methodBuilder.SetCustomAttribute(CreateAttributeBuilder());

                // Establish entry point Main
                assemblyBuilder.SetEntryPoint(methodBuilder);

                // Write the lines of code
                EmitCode(methodBuilder, toEmit);

                // Create the type
                typeBuilder.CreateType();

                // Save the dynamic assembly
                assemblyBuilder.Save(name);
            }

            private AssemblyBuilder CreateAssemblyBuilder(
            AssemblyName aName, AssemblyBuilderAccess access)
            {
                return AppDomain.CurrentDomain
                .DefineDynamicAssembly(aName, access);
            }

            private ModuleBuilder CreateModuleBuilder(
            AssemblyBuilder builder)
            {
                return assemblyBuilder.DefineDynamicModule("Class1.mod",
                "Hello.exe", false);
            }

            private TypeBuilder CreateTypeBuilder(ModuleBuilder builder)
            {
                return builder.DefineType("Class1");
            }

            private MethodBuilder CreateMethodBuilder(TypeBuilder builder)
            {
                return builder.DefineMethod("Main",
                MethodAttributes.Private | MethodAttributes.Static
                | MethodAttributes.HideBySig,
                CallingConventions.Standard, typeof(void),
                new Type[] { typeof(string[]) });
            }

            private CustomAttributeBuilder CreateAttributeBuilder()
            {
                return new CustomAttributeBuilder(
                typeof(System.STAThreadAttribute).GetConstructor(new Type[] { }),
                new object[] { });
            }

            private void EmitCode(MethodBuilder builder, string text)
            {
                ILGenerator generator = builder.GetILGenerator();
                generator.Emit(OpCodes.Ldstr, text);

                MethodInfo writeMethod = typeof(System.Console).GetMethod("WriteLine",BindingFlags.Public | BindingFlags.Static, null,
                            new Type[] { typeof(string) }, null);

                generator.Emit(OpCodes.Call, writeMethod);

                MethodInfo readMethod = typeof(System.Console).GetMethod("ReadKey"
                    ,BindingFlags.Public | BindingFlags.Static
                    ,Type.DefaultBinder
                    ,new Type[]{ } ,null );
                //generator.Emit(OpCodes.Ret);
                //generator.Emit(OpCodes.Call, readMethod);
                generator.Emit(OpCodes.Ret);
            }
        }
    }
}
