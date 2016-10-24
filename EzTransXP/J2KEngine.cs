using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace CodeGenerater.Translation.Plugins
{
	public class J2KEngine
	{
		#region Constructor
		public J2KEngine(string EzTransPath)
		{
			J2K_ENGINE_PATH = string.Format(@"{0}\{1}", EzTransPath, J2K_ENGINE_NAME);

			InitEventHandler();

			foreach (var each in GetFields()) InitDefferedLoader(each);
		}
		#endregion

		#region Field
		readonly string J2K_ENGINE_PATH;
		const string J2K_ENGINE_NAME = "J2KEngine.dll";
		EventHandler Handler;

		DeferredLoader<FreeMemDelegate> _J2K_FreeMem = null;
		DeferredLoader<Action> _J2K_GetPriorDict = null;
		DeferredLoader<Action> _J2K_GetProperty = null;
		DeferredLoader<Action> _J2K_Initialize = null;
		DeferredLoader<InitExDelegate> _J2K_InitializeEx = null;
		DeferredLoader<Action> _J2K_ReloadUserDict = null;
		DeferredLoader<Action> _J2K_SetDelJPN = null;
		DeferredLoader<Action> _J2K_SetField = null;
		DeferredLoader<Action> _J2K_SetHnj2han = null;
		DeferredLoader<Action> _J2K_SetJWin = null;
		DeferredLoader<Action> _J2K_SetPriorDict = null;
		DeferredLoader<Action> _J2K_SetProperty = null;
		DeferredLoader<Action> _J2K_StopTranslation = null;
		DeferredLoader<Action> _J2K_Terminate = null;
		DeferredLoader<Action> _J2K_TranslateChat = null;
		DeferredLoader<Action> _J2K_TranslateFM = null;
		DeferredLoader<Action> _J2K_TranslateMM = null;
		DeferredLoader<Action> _J2K_TranslateMMEx = null;
		DeferredLoader<TranslationDelegate> _J2K_TranslateMMNT = null;
		//DeferredLoader<TranslationDelegate> _J2K_TranslateMMNTW = null;
		#endregion

		#region Property
		public FreeMemDelegate J2K_FreeMem
		{
			get { return _J2K_FreeMem.Loaded; }
		}
		public Action J2K_GetPriorDict
		{
			get { return _J2K_GetPriorDict.Loaded; }
		}
		public Action J2K_GetProperty
		{
			get { return _J2K_GetProperty.Loaded; }
		}
		public Action J2K_Initialize
		{
			get { return _J2K_Initialize.Loaded; }
		}
		public InitExDelegate J2K_InitializeEx
		{
			get { return _J2K_InitializeEx.Loaded; }
		}
		public Action J2K_ReloadUserDict
		{
			get { return _J2K_ReloadUserDict.Loaded; }
		}
		public Action J2K_SetDelJPN
		{
			get { return _J2K_SetDelJPN.Loaded; }
		}
		public Action J2K_SetField
		{
			get { return _J2K_SetField.Loaded; }
		}
		public Action J2K_SetHnj2han
		{
			get { return _J2K_SetHnj2han.Loaded; }
		}
		public Action J2K_SetJWin
		{
			get { return _J2K_SetJWin.Loaded; }
		}
		public Action J2K_SetPriorDict
		{
			get { return _J2K_SetPriorDict.Loaded; }
		}
		public Action J2K_SetProperty
		{
			get { return _J2K_SetProperty.Loaded; }
		}
		public Action J2K_StopTranslation
		{
			get { return _J2K_StopTranslation.Loaded; }
		}
		public Action J2K_Terminate
		{
			get { return _J2K_Terminate.Loaded; }
		}
		public Action J2K_TranslateChat
		{
			get { return _J2K_TranslateChat.Loaded; }
		}
		public Action J2K_TranslateFM
		{
			get { return _J2K_TranslateFM.Loaded; }
		}
		public Action J2K_TranslateMM
		{
			get { return _J2K_TranslateMM.Loaded; }
		}
		public Action J2K_TranslateMMEx
		{
			get { return _J2K_TranslateMMEx.Loaded; }
		}
		public TranslationDelegate J2K_TranslateMMNT
		{
			get { return _J2K_TranslateMMNT.Loaded; }
		}
		public TranslationDelegate J2K_TranslateMMNTW
		{
			get { /*return null;*/ throw new NotSupportedException(); /*return _J2K_TranslateMMNTW.Loaded;*/ }
		}
		#endregion

		#region Helper
		void InitEventHandler()
		{
			Handler = (object src, EventArgs e) =>
			{
				FieldInfo Info = GetField(src);
				PropertyInfo LoadedInfo = Info.FieldType.GetProperty("Loaded");

				LoadedInfo.SetValue(Info.GetValue(this), Load(Info.Name.Remove(0, 1), LoadedInfo.PropertyType));
			};
		}

		FieldInfo GetField(object target)
		{
			return (from q in GetFields()
					where target == q.GetValue(this)
					select q).FirstOrDefault();
		}

		IEnumerable<FieldInfo> GetFields()
		{
			return from q in GetType().GetFields(BindingFlags.GetField | BindingFlags.NonPublic | BindingFlags.Instance)
				   where q.FieldType.IsGenericType select q;
		}

		object Load(string Name, Type DelegateType)
		{
			return LibraryLoader.Default.Load(J2K_ENGINE_PATH, Name, DelegateType);
		}

		void InitDefferedLoader(FieldInfo Target)
		{
			ConstructorInfo[] Info = Target.FieldType.GetConstructors();

			Target.SetValue(this, Target.FieldType.GetConstructor(Type.EmptyTypes).Invoke(null));

			Target.FieldType.GetEvent("Loading").AddEventHandler(Target.GetValue(this), Handler);
		}
		#endregion
	}
}