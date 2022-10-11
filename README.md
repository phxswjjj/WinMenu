## 目的
透過 xml 設定 Menu，設定內容包含 MenuItem、權限，MenuItem 可控制要開啟的 Form

## XML
```xml
<Menu>
	<Node Title="系統">
		<Node Title="Login" Type=".Menu.Login" ViewMode="NotLogin">
		</Node>
		<Node Title="Logout" Type=".Menu.Logout" ViewMode="OnlyLogin"></Node>
	</Node>
	<Node Title="Query">
		<Node Title="Query1" Type=".Menu.Query"></Node>
		<Node Title="Extand">
			<Node Title="Query1" ViewMode="Limit"></Node>
		</Node>
		<Node Title="QueryOnlyLogin" Type=".Menu.Query" ViewMode="Login" AccessString=":Query"></Node>
		<Node Title="QueryOnlyLimit" Type=".Menu.Query" ViewMode="Limit" AccessString="User:QueryX"></Node>
	</Node>
	<Node Title="Query2">
		<Node Title="Extand">
			<Node Title="QueryOnlyLimit" Type=".Menu.Query" ViewMode="Limit" AccessString=":Query"></Node>
		</Node>
		<Node Title="QueryOnlyLimit" Type=".Menu.Query" ViewMode="Limit" AccessString=":Query"></Node>
	</Node>
</Menu>
```

Type: 要執行的 MenuItem，由 「.」開頭表示為目前組件下的 namespace，否則為完整組件名稱
> Type=".Menu.Login"
等同 WinMenu.Menu.Login

## Menu
![](asserts/form-menu.jpg)
