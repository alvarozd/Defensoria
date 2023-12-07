                        FlechaSig.IsEnabled = false;
                        MessageBox.Show("el numero de registro 1 es = " +
                                        Registro1 + "\n el numero de registro 2 es = " +
                                        Registro2 + "\n el numero de registro 3 es = " +
                                        Registro3);
                        try
                        {
                            if (Registro1 == numeroRegistros - 1)
                            {
                                numeroRegistros = 1;
                                Registro1 = numeroRegistros;
                                PintaOperadores(numeroRegistros);
                            }
                            if (Registro2 == numeroRegistros - 1)
                            {
                                numeroRegistros = 2;
                                Registro1 = numeroRegistros - 1;
                                Registro2 = numeroRegistros;
                                PintaOperadores(numeroRegistros);
                            }

                            if (Registro3 == numeroRegistros -1)
                            {
//                                numeroRegistros = 3;
                                Registro1 = numeroRegistros - 2;
                                Registro2 = numeroRegistros - 1;
                                Registro3 = numeroRegistros;
                                PintaOperadores(numeroRegistros);
                            }
                            if (Registro3 < numeroRegistros - 1)
                            {
                                Registro1 = Registro1 + 3;
                                Registro2 = Registro2 + 3;
                                Registro3 = Registro3 + 3;
                                if ((Registro1 == numeroRegistros) || (Registro2 == numeroRegistros) || (Registro3 == numeroRegistros))
                                    Registro3 = numeroRegistros + 5;
                                PintaOperadores(numeroRegistros);
                            }
                            else
                            {
                                Registro1 = 0;
                                Registro2 = 1;
                                Registro3 = 2;
                                PintaOperadores(numeroRegistros);
                            }

