<Query Kind="Statements" />

Stopwatch watch = Stopwatch.StartNew();

// long operation here
Thread.Sleep(300);
watch.Stop();

watch.ElapsedMilliseconds.Dump("Elapsed (time)");
watch.ElapsedMilliseconds.Dump("Elapsed (ms)");
watch.ElapsedTicks.Dump("Elapsed (ticks)" );

watch.Start();
Thread.Sleep(200);
watch.Stop();

watch.ElapsedMilliseconds.Dump("Elapsed (time)");
watch.ElapsedMilliseconds.Dump("Elapsed (ms)");
watch.ElapsedTicks.Dump("Elapsed (ticks)" );
