// Copyright (C) 2015 Theunis Swanepoel. <theunis.swanepoel@gmail.com>
// This software is licensed under GNU LGPLv3.

namespace Biscuits.Scheduling
{
    using System;
    using System.Diagnostics;
    using System.Reactive.Subjects;
    using Windows.Foundation;
    using Windows.System.Threading;

    public static class SyncLoop
    {
        public static IAsyncAction ProduceAsync<T>(TimeSpan interval, Func<IAsyncAction, T> func, Subject<T> subject, WorkItemPriority priority)
        {
            return RunAsync(
                interval, 
                a => 
                {
                    T value;

                    try
                    {
                        value = func(a);
                    }
                    catch (Exception exception)
                    {
                        subject.OnError(exception);
                        return;
                    }

                    subject.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                }, 
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }
                    
                    subject5.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            TimeSpan interval10, Func<IAsyncAction, T10> func10, Subject<T10> subject10,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                }, 
                interval10,
                action =>
                {
                    T10 value;

                    try
                    {
                        value = func10(action);
                    }
                    catch (Exception exception)
                    {
                        subject10.OnError(exception);
                        return;
                    }

                    subject10.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            TimeSpan interval10, Func<IAsyncAction, T10> func10, Subject<T10> subject10,
            TimeSpan interval11, Func<IAsyncAction, T11> func11, Subject<T11> subject11,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                }, 
                interval10,
                action =>
                {
                    T10 value;

                    try
                    {
                        value = func10(action);
                    }
                    catch (Exception exception)
                    {
                        subject10.OnError(exception);
                        return;
                    }

                    subject10.OnNext(value);
                },
                interval11,
                action =>
                {
                    T11 value;

                    try
                    {
                        value = func11(action);
                    }
                    catch (Exception exception)
                    {
                        subject11.OnError(exception);
                        return;
                    }

                    subject11.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            TimeSpan interval10, Func<IAsyncAction, T10> func10, Subject<T10> subject10,
            TimeSpan interval11, Func<IAsyncAction, T11> func11, Subject<T11> subject11,
            TimeSpan interval12, Func<IAsyncAction, T12> func12, Subject<T12> subject12,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                },
                interval10,
                action =>
                {
                    T10 value;

                    try
                    {
                        value = func10(action);
                    }
                    catch (Exception exception)
                    {
                        subject10.OnError(exception);
                        return;
                    }

                    subject10.OnNext(value);
                },
                interval11,
                action =>
                {
                    T11 value;

                    try
                    {
                        value = func11(action);
                    }
                    catch (Exception exception)
                    {
                        subject11.OnError(exception);
                        return;
                    }

                    subject11.OnNext(value);
                },
                interval12,
                action =>
                {
                    T12 value;

                    try
                    {
                        value = func12(action);
                    }
                    catch (Exception exception)
                    {
                        subject12.OnError(exception);
                        return;
                    }

                    subject12.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            TimeSpan interval10, Func<IAsyncAction, T10> func10, Subject<T10> subject10,
            TimeSpan interval11, Func<IAsyncAction, T11> func11, Subject<T11> subject11,
            TimeSpan interval12, Func<IAsyncAction, T12> func12, Subject<T12> subject12,
            TimeSpan interval13, Func<IAsyncAction, T13> func13, Subject<T13> subject13,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                },
                interval10,
                action =>
                {
                    T10 value;

                    try
                    {
                        value = func10(action);
                    }
                    catch (Exception exception)
                    {
                        subject10.OnError(exception);
                        return;
                    }

                    subject10.OnNext(value);
                },
                interval11,
                action =>
                {
                    T11 value;

                    try
                    {
                        value = func11(action);
                    }
                    catch (Exception exception)
                    {
                        subject11.OnError(exception);
                        return;
                    }

                    subject11.OnNext(value);
                },
                interval12,
                action =>
                {
                    T12 value;

                    try
                    {
                        value = func12(action);
                    }
                    catch (Exception exception)
                    {
                        subject12.OnError(exception);
                        return;
                    }

                    subject12.OnNext(value);
                },
                interval13,
                action =>
                {
                    T13 value;

                    try
                    {
                        value = func13(action);
                    }
                    catch (Exception exception)
                    {
                        subject13.OnError(exception);
                        return;
                    }

                    subject13.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            TimeSpan interval10, Func<IAsyncAction, T10> func10, Subject<T10> subject10,
            TimeSpan interval11, Func<IAsyncAction, T11> func11, Subject<T11> subject11,
            TimeSpan interval12, Func<IAsyncAction, T12> func12, Subject<T12> subject12,
            TimeSpan interval13, Func<IAsyncAction, T13> func13, Subject<T13> subject13,
            TimeSpan interval14, Func<IAsyncAction, T14> func14, Subject<T14> subject14,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                },
                interval10,
                action =>
                {
                    T10 value;

                    try
                    {
                        value = func10(action);
                    }
                    catch (Exception exception)
                    {
                        subject10.OnError(exception);
                        return;
                    }

                    subject10.OnNext(value);
                },
                interval11,
                action =>
                {
                    T11 value;

                    try
                    {
                        value = func11(action);
                    }
                    catch (Exception exception)
                    {
                        subject11.OnError(exception);
                        return;
                    }

                    subject11.OnNext(value);
                },
                interval12,
                action =>
                {
                    T12 value;

                    try
                    {
                        value = func12(action);
                    }
                    catch (Exception exception)
                    {
                        subject12.OnError(exception);
                        return;
                    }

                    subject12.OnNext(value);
                },
                interval13,
                action =>
                {
                    T13 value;

                    try
                    {
                        value = func13(action);
                    }
                    catch (Exception exception)
                    {
                        subject13.OnError(exception);
                        return;
                    }

                    subject13.OnNext(value);
                },
                interval14,
                action =>
                {
                    T14 value;

                    try
                    {
                        value = func14(action);
                    }
                    catch (Exception exception)
                    {
                        subject14.OnError(exception);
                        return;
                    }

                    subject14.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            TimeSpan interval10, Func<IAsyncAction, T10> func10, Subject<T10> subject10,
            TimeSpan interval11, Func<IAsyncAction, T11> func11, Subject<T11> subject11,
            TimeSpan interval12, Func<IAsyncAction, T12> func12, Subject<T12> subject12,
            TimeSpan interval13, Func<IAsyncAction, T13> func13, Subject<T13> subject13,
            TimeSpan interval14, Func<IAsyncAction, T14> func14, Subject<T14> subject14,
            TimeSpan interval15, Func<IAsyncAction, T15> func15, Subject<T15> subject15,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                },
                interval10,
                action =>
                {
                    T10 value;

                    try
                    {
                        value = func10(action);
                    }
                    catch (Exception exception)
                    {
                        subject10.OnError(exception);
                        return;
                    }

                    subject10.OnNext(value);
                },
                interval11,
                action =>
                {
                    T11 value;

                    try
                    {
                        value = func11(action);
                    }
                    catch (Exception exception)
                    {
                        subject11.OnError(exception);
                        return;
                    }

                    subject11.OnNext(value);
                },
                interval12,
                action =>
                {
                    T12 value;

                    try
                    {
                        value = func12(action);
                    }
                    catch (Exception exception)
                    {
                        subject12.OnError(exception);
                        return;
                    }

                    subject12.OnNext(value);
                },
                interval13,
                action =>
                {
                    T13 value;

                    try
                    {
                        value = func13(action);
                    }
                    catch (Exception exception)
                    {
                        subject13.OnError(exception);
                        return;
                    }

                    subject13.OnNext(value);
                },
                interval14,
                action =>
                {
                    T14 value;

                    try
                    {
                        value = func14(action);
                    }
                    catch (Exception exception)
                    {
                        subject14.OnError(exception);
                        return;
                    }

                    subject14.OnNext(value);
                },
                interval15,
                action =>
                {
                    T15 value;

                    try
                    {
                        value = func15(action);
                    }
                    catch (Exception exception)
                    {
                        subject15.OnError(exception);
                        return;
                    }

                    subject15.OnNext(value);
                }, priority);
        }

        public static IAsyncAction ProduceAsync<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16>(
            TimeSpan interval1, Func<IAsyncAction, T1> func1, Subject<T1> subject1,
            TimeSpan interval2, Func<IAsyncAction, T2> func2, Subject<T2> subject2,
            TimeSpan interval3, Func<IAsyncAction, T3> func3, Subject<T3> subject3,
            TimeSpan interval4, Func<IAsyncAction, T4> func4, Subject<T4> subject4,
            TimeSpan interval5, Func<IAsyncAction, T5> func5, Subject<T5> subject5,
            TimeSpan interval6, Func<IAsyncAction, T6> func6, Subject<T6> subject6,
            TimeSpan interval7, Func<IAsyncAction, T7> func7, Subject<T7> subject7,
            TimeSpan interval8, Func<IAsyncAction, T8> func8, Subject<T8> subject8,
            TimeSpan interval9, Func<IAsyncAction, T9> func9, Subject<T9> subject9,
            TimeSpan interval10, Func<IAsyncAction, T10> func10, Subject<T10> subject10,
            TimeSpan interval11, Func<IAsyncAction, T11> func11, Subject<T11> subject11,
            TimeSpan interval12, Func<IAsyncAction, T12> func12, Subject<T12> subject12,
            TimeSpan interval13, Func<IAsyncAction, T13> func13, Subject<T13> subject13,
            TimeSpan interval14, Func<IAsyncAction, T14> func14, Subject<T14> subject14,
            TimeSpan interval15, Func<IAsyncAction, T15> func15, Subject<T15> subject15,
            TimeSpan interval16, Func<IAsyncAction, T16> func16, Subject<T16> subject16,
            WorkItemPriority priority)
        {
            return RunAsync(
                interval1,
                action =>
                {
                    T1 value;

                    try
                    {
                        value = func1(action);
                    }
                    catch (Exception exception)
                    {
                        subject1.OnError(exception);
                        return;
                    }

                    subject1.OnNext(value);
                },
                interval2,
                action =>
                {
                    T2 value;

                    try
                    {
                        value = func2(action);
                    }
                    catch (Exception exception)
                    {
                        subject2.OnError(exception);
                        return;
                    }

                    subject2.OnNext(value);
                },
                interval3,
                action =>
                {
                    T3 value;

                    try
                    {
                        value = func3(action);
                    }
                    catch (Exception exception)
                    {
                        subject3.OnError(exception);
                        return;
                    }

                    subject3.OnNext(value);
                },
                interval4,
                action =>
                {
                    T4 value;

                    try
                    {
                        value = func4(action);
                    }
                    catch (Exception exception)
                    {
                        subject4.OnError(exception);
                        return;
                    }

                    subject4.OnNext(value);
                },
                interval5,
                action =>
                {
                    T5 value;

                    try
                    {
                        value = func5(action);
                    }
                    catch (Exception exception)
                    {
                        subject5.OnError(exception);
                        return;
                    }

                    subject5.OnNext(value);
                },
                interval6,
                action =>
                {
                    T6 value;

                    try
                    {
                        value = func6(action);
                    }
                    catch (Exception exception)
                    {
                        subject6.OnError(exception);
                        return;
                    }

                    subject6.OnNext(value);
                },
                interval7,
                action =>
                {
                    T7 value;

                    try
                    {
                        value = func7(action);
                    }
                    catch (Exception exception)
                    {
                        subject7.OnError(exception);
                        return;
                    }

                    subject7.OnNext(value);
                },
                interval8,
                action =>
                {
                    T8 value;

                    try
                    {
                        value = func8(action);
                    }
                    catch (Exception exception)
                    {
                        subject8.OnError(exception);
                        return;
                    }

                    subject8.OnNext(value);
                },
                interval9,
                action =>
                {
                    T9 value;

                    try
                    {
                        value = func9(action);
                    }
                    catch (Exception exception)
                    {
                        subject9.OnError(exception);
                        return;
                    }

                    subject9.OnNext(value);
                },
                interval10,
                action =>
                {
                    T10 value;

                    try
                    {
                        value = func10(action);
                    }
                    catch (Exception exception)
                    {
                        subject10.OnError(exception);
                        return;
                    }

                    subject10.OnNext(value);
                },
                interval11,
                action =>
                {
                    T11 value;

                    try
                    {
                        value = func11(action);
                    }
                    catch (Exception exception)
                    {
                        subject11.OnError(exception);
                        return;
                    }

                    subject11.OnNext(value);
                },
                interval12,
                action =>
                {
                    T12 value;

                    try
                    {
                        value = func12(action);
                    }
                    catch (Exception exception)
                    {
                        subject12.OnError(exception);
                        return;
                    }

                    subject12.OnNext(value);
                },
                interval13,
                action =>
                {
                    T13 value;

                    try
                    {
                        value = func13(action);
                    }
                    catch (Exception exception)
                    {
                        subject13.OnError(exception);
                        return;
                    }

                    subject13.OnNext(value);
                },
                interval14,
                action =>
                {
                    T14 value;

                    try
                    {
                        value = func14(action);
                    }
                    catch (Exception exception)
                    {
                        subject14.OnError(exception);
                        return;
                    }

                    subject14.OnNext(value);
                },
                interval15,
                action =>
                {
                    T15 value;

                    try
                    {
                        value = func15(action);
                    }
                    catch (Exception exception)
                    {
                        subject15.OnError(exception);
                        return;
                    }

                    subject15.OnNext(value);
                },
                interval16,
                action =>
                {
                    T16 value;

                    try
                    {
                        value = func16(action);
                    }
                    catch (Exception exception)
                    {
                        subject16.OnError(exception);
                        return;
                    }

                    subject16.OnNext(value);
                }, priority);
        }

        public static IAsyncAction RunAsync(TimeSpan interval, Action<IAsyncAction> action, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw.Elapsed >= interval)
                        {
                            sw.Restart();
                            action(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9,
            TimeSpan interval10, Action<IAsyncAction> action10, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw10 = Stopwatch.StartNew();
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }

                        if (sw10.Elapsed >= interval10)
                        {
                            sw10.Restart();
                            action10(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9,
            TimeSpan interval10, Action<IAsyncAction> action10,
            TimeSpan interval11, Action<IAsyncAction> action11, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw11 = Stopwatch.StartNew();
                    var sw10 = Stopwatch.StartNew();
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }

                        if (sw10.Elapsed >= interval10)
                        {
                            sw10.Restart();
                            action10(handler);
                        }

                        if (sw11.Elapsed >= interval11)
                        {
                            sw11.Restart();
                            action11(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9,
            TimeSpan interval10, Action<IAsyncAction> action10,
            TimeSpan interval11, Action<IAsyncAction> action11,
            TimeSpan interval12, Action<IAsyncAction> action12, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw12 = Stopwatch.StartNew();
                    var sw11 = Stopwatch.StartNew();
                    var sw10 = Stopwatch.StartNew();
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }

                        if (sw10.Elapsed >= interval10)
                        {
                            sw10.Restart();
                            action10(handler);
                        }

                        if (sw11.Elapsed >= interval11)
                        {
                            sw11.Restart();
                            action11(handler);
                        }

                        if (sw12.Elapsed >= interval12)
                        {
                            sw12.Restart();
                            action12(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9,
            TimeSpan interval10, Action<IAsyncAction> action10,
            TimeSpan interval11, Action<IAsyncAction> action11,
            TimeSpan interval12, Action<IAsyncAction> action12,
            TimeSpan interval13, Action<IAsyncAction> action13, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw13 = Stopwatch.StartNew();
                    var sw12 = Stopwatch.StartNew();
                    var sw11 = Stopwatch.StartNew();
                    var sw10 = Stopwatch.StartNew();
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }

                        if (sw10.Elapsed >= interval10)
                        {
                            sw10.Restart();
                            action10(handler);
                        }

                        if (sw11.Elapsed >= interval11)
                        {
                            sw11.Restart();
                            action11(handler);
                        }

                        if (sw12.Elapsed >= interval12)
                        {
                            sw12.Restart();
                            action12(handler);
                        }

                        if (sw13.Elapsed >= interval13)
                        {
                            sw13.Restart();
                            action13(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9,
            TimeSpan interval10, Action<IAsyncAction> action10,
            TimeSpan interval11, Action<IAsyncAction> action11,
            TimeSpan interval12, Action<IAsyncAction> action12,
            TimeSpan interval13, Action<IAsyncAction> action13,
            TimeSpan interval14, Action<IAsyncAction> action14, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw14 = Stopwatch.StartNew();
                    var sw13 = Stopwatch.StartNew();
                    var sw12 = Stopwatch.StartNew();
                    var sw11 = Stopwatch.StartNew();
                    var sw10 = Stopwatch.StartNew();
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }

                        if (sw10.Elapsed >= interval10)
                        {
                            sw10.Restart();
                            action10(handler);
                        }

                        if (sw11.Elapsed >= interval11)
                        {
                            sw11.Restart();
                            action11(handler);
                        }

                        if (sw12.Elapsed >= interval12)
                        {
                            sw12.Restart();
                            action12(handler);
                        }

                        if (sw13.Elapsed >= interval13)
                        {
                            sw13.Restart();
                            action13(handler);
                        }

                        if (sw14.Elapsed >= interval14)
                        {
                            sw14.Restart();
                            action14(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9,
            TimeSpan interval10, Action<IAsyncAction> action10,
            TimeSpan interval11, Action<IAsyncAction> action11,
            TimeSpan interval12, Action<IAsyncAction> action12,
            TimeSpan interval13, Action<IAsyncAction> action13,
            TimeSpan interval14, Action<IAsyncAction> action14,
            TimeSpan interval15, Action<IAsyncAction> action15, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw15 = Stopwatch.StartNew();
                    var sw14 = Stopwatch.StartNew();
                    var sw13 = Stopwatch.StartNew();
                    var sw12 = Stopwatch.StartNew();
                    var sw11 = Stopwatch.StartNew();
                    var sw10 = Stopwatch.StartNew();
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }

                        if (sw10.Elapsed >= interval10)
                        {
                            sw10.Restart();
                            action10(handler);
                        }

                        if (sw11.Elapsed >= interval11)
                        {
                            sw11.Restart();
                            action11(handler);
                        }

                        if (sw12.Elapsed >= interval12)
                        {
                            sw12.Restart();
                            action12(handler);
                        }

                        if (sw13.Elapsed >= interval13)
                        {
                            sw13.Restart();
                            action13(handler);
                        }

                        if (sw14.Elapsed >= interval14)
                        {
                            sw14.Restart();
                            action14(handler);
                        }

                        if (sw15.Elapsed >= interval15)
                        {
                            sw15.Restart();
                            action15(handler);
                        }
                    }
                }, priority);
        }

        public static IAsyncAction RunAsync(
            TimeSpan interval1, Action<IAsyncAction> action1,
            TimeSpan interval2, Action<IAsyncAction> action2,
            TimeSpan interval3, Action<IAsyncAction> action3,
            TimeSpan interval4, Action<IAsyncAction> action4,
            TimeSpan interval5, Action<IAsyncAction> action5,
            TimeSpan interval6, Action<IAsyncAction> action6,
            TimeSpan interval7, Action<IAsyncAction> action7,
            TimeSpan interval8, Action<IAsyncAction> action8,
            TimeSpan interval9, Action<IAsyncAction> action9,
            TimeSpan interval10, Action<IAsyncAction> action10,
            TimeSpan interval11, Action<IAsyncAction> action11,
            TimeSpan interval12, Action<IAsyncAction> action12,
            TimeSpan interval13, Action<IAsyncAction> action13,
            TimeSpan interval14, Action<IAsyncAction> action14,
            TimeSpan interval15, Action<IAsyncAction> action15,
            TimeSpan interval16, Action<IAsyncAction> action16, WorkItemPriority priority)
        {
            return ThreadPool.RunAsync(
                handler =>
                {
                    var sw16 = Stopwatch.StartNew();
                    var sw15 = Stopwatch.StartNew();
                    var sw14 = Stopwatch.StartNew();
                    var sw13 = Stopwatch.StartNew();
                    var sw12 = Stopwatch.StartNew();
                    var sw11 = Stopwatch.StartNew();
                    var sw10 = Stopwatch.StartNew();
                    var sw9 = Stopwatch.StartNew();
                    var sw8 = Stopwatch.StartNew();
                    var sw7 = Stopwatch.StartNew();
                    var sw6 = Stopwatch.StartNew();
                    var sw5 = Stopwatch.StartNew();
                    var sw4 = Stopwatch.StartNew();
                    var sw3 = Stopwatch.StartNew();
                    var sw2 = Stopwatch.StartNew();
                    var sw1 = Stopwatch.StartNew();

                    while (true)
                    {
                        if (sw1.Elapsed >= interval1)
                        {
                            sw1.Restart();
                            action1(handler);
                        }

                        if (sw2.Elapsed >= interval2)
                        {
                            sw2.Restart();
                            action2(handler);
                        }

                        if (sw3.Elapsed >= interval3)
                        {
                            sw3.Restart();
                            action3(handler);
                        }

                        if (sw4.Elapsed >= interval4)
                        {
                            sw4.Restart();
                            action4(handler);
                        }

                        if (sw5.Elapsed >= interval5)
                        {
                            sw5.Restart();
                            action5(handler);
                        }

                        if (sw6.Elapsed >= interval6)
                        {
                            sw6.Restart();
                            action6(handler);
                        }

                        if (sw7.Elapsed >= interval7)
                        {
                            sw7.Restart();
                            action7(handler);
                        }

                        if (sw8.Elapsed >= interval8)
                        {
                            sw8.Restart();
                            action8(handler);
                        }

                        if (sw9.Elapsed >= interval9)
                        {
                            sw9.Restart();
                            action9(handler);
                        }

                        if (sw10.Elapsed >= interval10)
                        {
                            sw10.Restart();
                            action10(handler);
                        }

                        if (sw11.Elapsed >= interval11)
                        {
                            sw11.Restart();
                            action11(handler);
                        }

                        if (sw12.Elapsed >= interval12)
                        {
                            sw12.Restart();
                            action12(handler);
                        }

                        if (sw13.Elapsed >= interval13)
                        {
                            sw13.Restart();
                            action13(handler);
                        }

                        if (sw14.Elapsed >= interval14)
                        {
                            sw14.Restart();
                            action14(handler);
                        }

                        if (sw15.Elapsed >= interval15)
                        {
                            sw15.Restart();
                            action15(handler);
                        }

                        if (sw16.Elapsed >= interval16)
                        {
                            sw16.Restart();
                            action16(handler);
                        }
                    }
                }, priority);
        }
    }
}
