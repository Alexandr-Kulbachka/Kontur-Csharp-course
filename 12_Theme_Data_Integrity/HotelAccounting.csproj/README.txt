В файле AccountingModel.cs создайте класс AccountingModel, унаследованный от ModelBase, со следующими свойствами.

    double Price — цена за одну ночь. Должна быть неотрицательной.
    int NightsCount — количество ночей. Должно быть положительным.
    double Discount — скидка в процентах. Никаких дополнительных ограничений.
    double Total — сумма счёта. Должна быть не отрицательна и должна быть согласована с предыдущими тремя свойствами по 
правилу: Total == Price * NightsCount * (1-Discount/100).

Все поля должны иметь сеттеры. При установке Price, NightsCount и Discount должна соответствующим образом устанавливаться 
Total, при установке Total — соответствующим образом меняться Discount. В случае установки значения, нарушающего хоть одно
 условие целостности, необходимо выкидывать ArgumentException.

При изменении значения любого свойства необходимо дополнительно сигнализировать об этом с помощью вызова метода Notify,
 передавая ему имя изменяемого свойства. Здесь можно воспользоваться ключевым словом nameof.
Обсуждение

Ситуация, когда свойства вот так взаимозависимы и при этом каждое имеет сеттер, довольно редка. В норме, Total бы имело 
только геттер. Однако, классы такого вида тоже встречаются, например в качестве класса-модели для пользовательского 
интерфейса. Библиотека WPF и некоторые другие библиотеки для построения пользовательских интерфейсов дают возможность 
так называемого двухстороннего связывания контролах окна со свойствами в объекте модели. При изменении значения в 
контролах меняются значения свойств объекта, и наоборот, при изменении свойств меняются значения в контролах.

Загляните в файлы MainWindow.xaml и MainWindow.cs, вы увидите, что там нет кода для обновления полей, пересчёта значений 
и выделения поля с ошибочным вводом. Формат Xaml скорее всего вам не знаком, но тем не менее убедиться, что там нет этой 
логики несложно. Зато можно увидеть как устанавливается связь между полями графического интерфейса и свойствами модели. 
Такой подход называется View-ViewModel, в котором MainForm — это View, а сам класс AccountingModel — ViewModel.

WPF — библиотека предназначенная только для Windows. Поэтому на не windows компьютерах вам не удастся увидеть, как именно 
работает MVVM-связка, но вы можете написать требуемый класс, создав новый проект.