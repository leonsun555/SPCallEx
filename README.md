# SPCallEx
--透過動態object存放呼叫預儲程序的結果

# 問題點
--ASP.NET Core 後台程式可以利用EntityFramework串起與Database的溝通橋梁,不管是CodeFirst或是DBFFirst都有不同的愛用族群及適用的地方,
但是DB端的Storeprocedure對於寫後台程式的人是個很大的問題,因為SP輸出的欄位可能會是動態產生,不像是靜態的Table和View,如此在程式端就需要
因應SP輸出的欄位產生相對應的class來存放sp產出的欄位,這樣豈不是要定義所有SP有可能輸出的欄位名稱才可以做到data binding,簡單的sp是沒甚麼
問題,複雜的sp就不太可能產生巨量的class member,我之前的作法是把SP直接轉寫成ASP.NET程式的Method,但還是太過於麻煩,才發現了透過動態object
方法來解決我的問題.

# 事前準備
1. ASP.NET Core 2.2 Web api
2. Nuget package: 
  --Microsoft.EntityFrameworkCore.SqlServer
  --Microsoft.EntityFrameworkCore.Tools
  --Microsoft.EntityFrameworkCore.SqlServer.Design
3. 建立測試StoreProcedure

# Step by Step
1. 下載上述Nuget package
2. 開啟"套件管理器主控台",輸入:Scaffold-DbContext "Server=yourDbserver;Database=yourDatabase;Trusted_Connection=True;user id=SQL USERNAME;password=SQLPASSWORD;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models 
3. 將Scaffold產生的DbContext注入services提供服務:
  services.AddDbContext<DBContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Server=yourDbserver;Database=yourDatabase;user id=SQLUSERNAME;password=SQLPASSWORD;")));
4. 參照原碼內容完成Call SP Api端點
