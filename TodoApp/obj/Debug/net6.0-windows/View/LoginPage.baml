   M S B A M L   `   `   `  ????B  >TodoApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null+clr-namespace:TodoApp.ViewTodoApp.View  5      7 clr-namespace:TodoApp.ControllerTodoApp.Controller  5          TodoApp.View.LoginPage   5      W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ,localclr-namespace:TodoApp.View     5      7
controller clr-namespace:TodoApp.Controller     5        N? Title$  	LoginPage??5	      $	??300??6   $	??450??6&    ? DataContext 5
      (   "TodoApp.Controller.LoginController  5   
   .??5      ?  ? RowDefinitions 5   
   ?? 5      $
??Auto ?6   ?? 5      $??* ?6   .+?5   
   ?? $??0??6   $??10q?6"   .+?5      ?? $??TODO App??6   $??24(?6(   $
??Bold"?66   ?? 5      $??Please login to continue.??6   $??0 5 0 0q?69   5      ?? 5   
   $??1??6   ???#F0F0F0$??10q?63    ?? CornerRadius$ 5??6?   ._?5      ?  5      ?? 5      $
??Auto ?6$   ?? 5      $
??Auto ?6$   ?? 5      $
??Auto ?6$   ?? 5      $
??Auto ?6$   ?? 5      $
??Auto ?6$   ?? 5       $
??Auto ?6$    ? ColumnDefinitions 5#      ?? 5$      $	??100 ?6'   ?? 5%      $??* ?6'   .+?5(      ?? $??	Username:??6   $??10q?6-   $??0??69   $??0??6F   $??Center=?6V   ?? 5*      -   5         ??Name$ Username??5*   C   $??0??6   $??1??6'   $??10q?67   $??25??6o   $??Center=?6{   ??6U   ?? *
Username+6{   ?? 5,      $??	Password:??6   $??
10 5 10 10q?6-   $??1??6A   $??0??6N   $??Center=?6^   G? 5-      -   5         G?Name$ Password??5-   <   $??1??6   $??1??6+    G? PasswordChar$ *??6N   $??
10 5 10 10q?6_   $??25??6s   $??Center=?6   ?? 5/      $??Login??6   $??10 20 10 10q?6)   $??4??6>   $??1??6K   $??Right=?6[   $	??100??6w   $??30??6?      ModernButtonStyle#??[  6?   ??6?   ?? *LoginCommand+??6?   ??  ?? ElementName$ Password??6?   50      51      52      53      