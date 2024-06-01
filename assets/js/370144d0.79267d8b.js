"use strict";(self.webpackChunkdocs=self.webpackChunkdocs||[]).push([[5346],{3905:(e,t,n)=>{n.d(t,{Zo:()=>p,kt:()=>h});var i=n(7294);function a(e,t,n){return t in e?Object.defineProperty(e,t,{value:n,enumerable:!0,configurable:!0,writable:!0}):e[t]=n,e}function s(e,t){var n=Object.keys(e);if(Object.getOwnPropertySymbols){var i=Object.getOwnPropertySymbols(e);t&&(i=i.filter((function(t){return Object.getOwnPropertyDescriptor(e,t).enumerable}))),n.push.apply(n,i)}return n}function r(e){for(var t=1;t<arguments.length;t++){var n=null!=arguments[t]?arguments[t]:{};t%2?s(Object(n),!0).forEach((function(t){a(e,t,n[t])})):Object.getOwnPropertyDescriptors?Object.defineProperties(e,Object.getOwnPropertyDescriptors(n)):s(Object(n)).forEach((function(t){Object.defineProperty(e,t,Object.getOwnPropertyDescriptor(n,t))}))}return e}function o(e,t){if(null==e)return{};var n,i,a=function(e,t){if(null==e)return{};var n,i,a={},s=Object.keys(e);for(i=0;i<s.length;i++)n=s[i],t.indexOf(n)>=0||(a[n]=e[n]);return a}(e,t);if(Object.getOwnPropertySymbols){var s=Object.getOwnPropertySymbols(e);for(i=0;i<s.length;i++)n=s[i],t.indexOf(n)>=0||Object.prototype.propertyIsEnumerable.call(e,n)&&(a[n]=e[n])}return a}var l=i.createContext({}),c=function(e){var t=i.useContext(l),n=t;return e&&(n="function"==typeof e?e(t):r(r({},t),e)),n},p=function(e){var t=c(e.components);return i.createElement(l.Provider,{value:t},e.children)},u="mdxType",d={inlineCode:"code",wrapper:function(e){var t=e.children;return i.createElement(i.Fragment,{},t)}},m=i.forwardRef((function(e,t){var n=e.components,a=e.mdxType,s=e.originalType,l=e.parentName,p=o(e,["components","mdxType","originalType","parentName"]),u=c(n),m=a,h=u["".concat(l,".").concat(m)]||u[m]||d[m]||s;return n?i.createElement(h,r(r({ref:t},p),{},{components:n})):i.createElement(h,r({ref:t},p))}));function h(e,t){var n=arguments,a=t&&t.mdxType;if("string"==typeof e||a){var s=n.length,r=new Array(s);r[0]=m;var o={};for(var l in t)hasOwnProperty.call(t,l)&&(o[l]=t[l]);o.originalType=e,o[u]="string"==typeof e?e:a,r[1]=o;for(var c=2;c<s;c++)r[c]=n[c];return i.createElement.apply(null,r)}return i.createElement.apply(null,n)}m.displayName="MDXCreateElement"},2785:(e,t,n)=>{n.r(t),n.d(t,{assets:()=>l,contentTitle:()=>r,default:()=>d,frontMatter:()=>s,metadata:()=>o,toc:()=>c});var i=n(7462),a=(n(7294),n(3905));const s={sidebar_position:1},r="Editor Extensions",o={unversionedId:"utilities/editor-extensions",id:"utilities/editor-extensions",title:"Editor Extensions",description:"Kassets provides a set of editor extensions to help you work with Kassets more efficiently.",source:"@site/docs/5-utilities/editor-extensions.md",sourceDirName:"5-utilities",slug:"/utilities/editor-extensions",permalink:"/Kassets/utilities/editor-extensions",draft:!1,editUrl:"https://github.com/kadinche/Kassets/tree/docs/main/Documentation/docs/5-utilities/editor-extensions.md",tags:[],version:"current",sidebarPosition:1,frontMatter:{sidebar_position:1},sidebar:"tutorialSidebar",previous:{title:"Creating Custom Class",permalink:"/Kassets/fundamentals/creating-custom-class"},next:{title:"JSON Extensions",permalink:"/Kassets/utilities/json-extension"}},l={},c=[{value:"Create Kassets Instances from Inspector",id:"create-kassets-instances-from-inspector",level:2},{value:"Modify Kassets Instances from referencing Components",id:"modify-kassets-instances-from-referencing-components",level:2},{value:"Raise Events from Inspector",id:"raise-events-from-inspector",level:2},{value:"Customize Instance&#39;s Behavior",id:"customize-instances-behavior",level:2}],p={toc:c},u="wrapper";function d(e){let{components:t,...n}=e;return(0,a.kt)(u,(0,i.Z)({},p,n,{components:t,mdxType:"MDXLayout"}),(0,a.kt)("h1",{id:"editor-extensions"},"Editor Extensions"),(0,a.kt)("p",null,"Kassets provides a set of editor extensions to help you work with Kassets more efficiently.\nThese extensions are designed to help you create, manage, and debug Kassets instances."),(0,a.kt)("h2",{id:"create-kassets-instances-from-inspector"},"Create Kassets Instances from Inspector"),(0,a.kt)("p",null,"If you have a ",(0,a.kt)("inlineCode",{parentName:"p"},"ScriptableObject")," field in your ",(0,a.kt)("inlineCode",{parentName:"p"},"MonoBehaviour")," script,\nyou can create a new instance by clicking the ",(0,a.kt)("inlineCode",{parentName:"p"},"Create")," button in the inspector.\nThe new instance will be automatically assigned to the field.\n",(0,a.kt)("inlineCode",{parentName:"p"},"Create")," button will appear when the field is empty."),(0,a.kt)("p",null,(0,a.kt)("img",{parentName:"p",src:"https://github.com/kadinche/Kassets/assets/1290720/99812122-b7c7-42ff-a2e2-8985ff90b77e",alt:"Screenshot 2024-05-20 at 4 08 51"})),(0,a.kt)("h2",{id:"modify-kassets-instances-from-referencing-components"},"Modify Kassets Instances from referencing Components"),(0,a.kt)("p",null,"An editor extension is provided to modify the Kassets instances directly from the referencing components.\nThere's a dropdown to toggle and view the scriptable object's fields.\nThis reduces the need to select the scriptable object asset in the project view."),(0,a.kt)("p",null,(0,a.kt)("img",{parentName:"p",src:"https://github.com/kadinche/Kassets/assets/1290720/7cb7669a-958a-4b91-82af-9e17c833fb34",alt:"Screenshot 2024-05-21 at 4 43 28"})),(0,a.kt)("h2",{id:"raise-events-from-inspector"},"Raise Events from Inspector"),(0,a.kt)("p",null,"Instances of ",(0,a.kt)("inlineCode",{parentName:"p"},"GameEvent")," and its derived classes can have their events raised directly from the inspector.\nThis can be useful for debugging or testing purposes.\nSimply click the ",(0,a.kt)("inlineCode",{parentName:"p"},"Raise")," button in the inspector to raise the event.\nThe ",(0,a.kt)("inlineCode",{parentName:"p"},"Raise")," button is enabled on Play mode."),(0,a.kt)("p",null,(0,a.kt)("img",{parentName:"p",src:"https://github.com/kadinche/Kassets/assets/1290720/457d92df-5057-4e90-a9db-9adfef04c740",alt:"Screenshot 2024-05-20 at 4 56 16"})),(0,a.kt)("h2",{id:"customize-instances-behavior"},"Customize Instance's Behavior"),(0,a.kt)("p",null,"Each instance of ",(0,a.kt)("inlineCode",{parentName:"p"},"Kassets")," can have its behavior customized from the inspector.\nCustomizable behaviors vary depending on the instance type."),(0,a.kt)("p",null,(0,a.kt)("img",{parentName:"p",src:"https://github.com/kadinche/Kassets/assets/1290720/f7a3d5c4-123e-4c04-ab53-c51baf168241",alt:"Screenshot 2024-05-20 at 4 57 46"})),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"GameEvent")),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("inlineCode",{parentName:"li"},"Subscribe Behavior")," : Select how the event should behave when it is raised, whether ",(0,a.kt)("inlineCode",{parentName:"li"},"Pull")," or ",(0,a.kt)("inlineCode",{parentName:"li"},"Push"),".",(0,a.kt)("ul",{parentName:"li"},(0,a.kt)("li",{parentName:"ul"},"Note : This setting is only available when both UniRx and UniTask are imported.")))),(0,a.kt)("p",null,(0,a.kt)("strong",{parentName:"p"},"Variable")),(0,a.kt)("ul",null,(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("inlineCode",{parentName:"li"},"Variable Event Type")," : Select the event type that will be raised on variable events.",(0,a.kt)("ul",{parentName:"li"},(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("inlineCode",{parentName:"li"},"ValueAssign")," : Raise an event when the variable value is assigned, regardless of whether the value changes."),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("inlineCode",{parentName:"li"},"ValueChanged")," : Raise an event only when the variable value changes."))),(0,a.kt)("li",{parentName:"ul"},(0,a.kt)("inlineCode",{parentName:"li"},"Auto Reset Value")," : Reset the variable value to its initial value when Play Mode is stopped.\nInitial Value is defined as the value set before entering Play Mode.")),(0,a.kt)("admonition",{type:"note"},(0,a.kt)("p",{parentName:"admonition"},"By Unity's Scriptable Object default behavior, any changes made to the Scriptable Object instance in Play Mode will be saved.\n",(0,a.kt)("inlineCode",{parentName:"p"},"Auto Reset Value")," allows you to reset the value to the initial value when Play Mode is stopped.")))}d.isMDXComponent=!0}}]);