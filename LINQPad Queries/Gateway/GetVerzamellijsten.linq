<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Runtime.Serialization.Formatters.Soap.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Converters</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net</Namespace>
  <Namespace>System.Net.Http</Namespace>
  <Namespace>System.Net.Http.Headers</Namespace>
</Query>

var puertoEndpoint = "http://10.10.40.152/";
var token = @"dAc_Mqdnc67d3B_mu8dr1Sr-kpwPehqtIHbefCyg8vuE8SqaVZ1CQF61JmXyFOyzm_9OxlXVzJ2HSVTEuYetlIDaqWhoQ9lsRKY_Ba93GNCzmCEv7i2L44DecS4DZd7_jT0eGLLdsTB-vfzE1L52u3Fc53HssVqhRHFSihJbA4gm2U0q-AxVikYqSCiS5EErE9PwSFctYDeOapVKBuZZlj6mXo9C4b1SgbQ3rwFW2Yr8YcAW_w80DIWVq7KrotEI7lZ0aJdWgad02p9v45ZBE7LCt9-wEoKAcqvxuf-wqWJG03cb8qS1lxHQesdPpddOXL3aAoRbYtcjp0bv34GHnte8K1cw-0SNeG_6dkTbA52Ow6if4V78J5cf4_95Onz6JUpW6dopguQ-3NU5gSH3cPHjdpi-BvDDRTbUBl5NpW3DqLBHIPwNQCj6ugUK50IlmxCuvPzOZgiNkV9V6fZmswTyEDl6vP3rE5CO8QpvmjC4bb0zQK5aqD1j6zKUM0Z_mQbSHxDZIqCpi66crcX0N9UEYS62JmWAzkY_Kj2NvHM";

var loginClient = new HttpClient { BaseAddress = new Uri(puertoEndpoint) };
loginClient.DefaultRequestHeaders.Accept.Clear();
loginClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
loginClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

var requestData = new {_AdminContext = new {AdminCode = "demoart"}};

var content = new StringContent(JsonConvert.SerializeObject(requestData).Dump("JSON"));
content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

HttpResponseMessage responseMessage = await loginClient.PostAsync("api/Verzamellijst/GetVerzamellijsten/", content);            
var responseString = await responseMessage.Content.ReadAsStringAsync();

if (responseMessage.StatusCode == HttpStatusCode.OK)
{
  responseString.Dump("Response");
}
else
{
  responseMessage.Dump("Response message");
  responseString.Dump("Response content");
}