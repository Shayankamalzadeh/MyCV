function integerNumber (input) {
				var answer= true;
				if(!parseInt (input)) {
		answer = false;
                 					} else
										{

										for (var i=0; i<input.length; i++)
											{
												if ((input.charAt(i) != "0")
												&& (! parseInt (input.charAt (i))) ) {
		answer = false;
												break;
										}
									}
								}
				return answer;
				}

				function calculateYears()
				{
				if (! integerNumber (document.registration.birthyear.value))
				{
		alert("Wrong birth year!");
					document.registration.birthyear.value = "";
					return false;
				}
				else {
						var date = new Date();
						document.registration.years.value= date.getFullYear() -	document.registration.birthyear.value;

				return true;
				}
				}

				function exists (input)
				{
					var atleastOneChar = false;

					if (input)
					{
				     for (var i=0; i<input.length; i++)
					 {
				     if (input.charAt(i) != " ")
						{
		atleastOneChar = true;
						 break;
						}
					}

					}

					return atleastOneChar;

				}


				function checkForm() {

					var mes = "";

					if (!exists (document.registration.surname.value))
					mes = mes + "Missing family name\n";

					if (!exists (document.registration.name.value))

					mes =mes + "Missing first name";



					if (mes !="")
					alert (mes);

					res =calculateYears();
					if ((mes== "") && (res))

					alert ("Ok, correct!\nThe form should now be sent to " +
					"the server through "+
					"and Create a Sample xml and download it");

				}

				function draw() {

		canvas = document.getElementById('canv');
					ctx= canvas.getContext('2d');
					ctx.fillstyle='#0101fe';
					ctx.fillRect (50, 60, 100, 100);
					ctx.fillstyle="rgba(220, 223, 0, 0.5)";
					ctx.fillRect (90, 105, 100, 80);
					}