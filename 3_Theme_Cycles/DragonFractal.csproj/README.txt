Скачайте проект DragonFractal

В этой задаче вам нужно будет нарисовать вот такую фигуру:

Dragon curve

Вряд ли это пригодится вам в будущем, но зато красиво! :)

Подробнее про этот фрактал можете почитать, например, в википедии.

Алгоритм построения фрактала читайте в комментариях в классе DragonFractalTask

Кстати, похожим образом можно построить ещё множество фракталов, в частности, фрактальный папоротник:
Как генерировать случайные числа?

Для этого в пространстве имен System есть класс Random. Работать с ним нужно так:

1

// 1. Создание нового генератора последовательности случайных чисел:

2

var random = new Random(seed);

3

// seed — число полностью определяющее все последовательность псевдослучайных чисел этого генератора.

4

​

5

// 2. Получение очередного псевдослучайного числа от 0 до 9:

6

var nextNumber = random.Next(10);

Если при инициализации генератора случайных чисел не указывать seed, то используется текущее время компьютера с точностью 
до миллисекунд. Поэтому если вы создадите два генератора подряд, то с большой вероятностью они проинициализируются 
одинаково и будут выдавать одну и ту же последовательность.

Типичная ошибка начинающих — поместить обе операции внутрь цикла, тогда как правильно вынести создание генератора за 
пределы цикла, оставив внутри только получение следующего числа.