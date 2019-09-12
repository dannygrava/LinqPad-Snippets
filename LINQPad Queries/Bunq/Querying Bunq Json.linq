<Query Kind="Statements">
  <Reference>&lt;ProgramFilesX64&gt;\Microsoft SDKs\Azure\.NET SDK\v2.9\bin\plugins\Diagnostics\Newtonsoft.Json.dll</Reference>
  <Reference>&lt;RuntimeDirectory&gt;\System.Net.Http.dll</Reference>
  <Namespace>Newtonsoft.Json</Namespace>
  <Namespace>Newtonsoft.Json.Linq</Namespace>
  <Namespace>System.Globalization</Namespace>
  <Namespace>System.Net.Http</Namespace>
</Query>

string json = "{\r\n\t\"Error\": [{\r\n\t\t\t\"error_description\": \"Error description\",\r\n\t\t\t\"error_description_translated\": \"User facing error description\"\r\n\t\t}\r\n\t]\r\n}";
	json = @"{
	""Response"": [{
			""Id"": {
				""id"": 1033
			}
		}, {
			""Token"": {
				""id"": 1815,
				""created"": ""2017-02-02 08:38:07.951755"",
				""updated"": ""2017-02-02 08:38:07.951755"",
				""token"": ""2173a923b3275dd1b951f83ffcaac46967ecfa3142c87c940f7e6f3fd744409f""
			}
		}, {
			""ServerPublicKey"": {
				""server_public_key"": ""-----BEGIN PUBLIC KEY-----\nMIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAsZMvTRymmWXiY1CF+GSv\nLkODgj0EuSdvrr2HrqcD4XfhnK9P33gv4\/2\/WXYWW4IlNri\/d109wCPlbeF76GoG\ncC+C4aDEm9qmlK2sFV97aVe2TTJLhb67A1v7Fzk021LKIp3rdX8nZ5BdZ7MK7l9e\n05dMv8zd7\/mzlDjJPRYBXZjuMmKMKO54h09CbxjVCwkNOyTMLSwiykQT7OTT4vDI\nPSdLQqfoC1fyHIz2dT2TwAAsWxZ73JzX1vNEONZ5fXDGs6TkVODK3GQoyuPM1sEa\nuwX87z7+S9kVKrYkr+EE4MTdgcUyUGk1CpHAJoQmxxdBA9pWlR0Dqx9Yai53amV3\n\/QIDAQAB\n-----END PUBLIC KEY-----\n""
			}
		}
	]
}
";

json = @"{""Response"":[{""ShareInviteBankResponse"":{""id"":266,""created"":""2017-02-14 09:45:52.832207"",""updated"":""2017-02-14 09:45:52.832207"",""monetary_account_id"":779,""draft_share_invite_bank_id"":22975,""counter_alias"":{""iban"":""NL72BUNQ9900006755"",""is_light"":false,""display_name"":""Arcelia Bell"",""avatar"":{""uuid"":""26db6bcd-be2e-4ff4-86c6-e409179a4bf6"",""image"":[{""attachment_public_uuid"":""80a1fb50-e1e4-4dae-a92c-ef7458d129c5"",""height"":1024,""width"":1024,""content_type"":""image\/png""}],""anchor_uuid"":null},""label_user"":{""uuid"":""82433943-4a15-4023-95bc-cb250dcecb59"",""display_name"":""A. Bell"",""country"":""NL"",""nick_name"":""Arcelia (nickname)"",""avatar"":{""uuid"":""d4a33505-090d-4a7c-ba56-c9c8c354058e"",""image"":[{""attachment_public_uuid"":""b64e74a2-6a5b-47a2-a863-d64410b85697"",""height"":480,""width"":480,""content_type"":""image\/jpeg""}],""anchor_uuid"":""82433943-4a15-4023-95bc-cb250dcecb59""}},""country"":""NL""},""status"":""ACCEPTED"",""share_detail"":{""ShareDetailReadOnly"":{""view_balance"":true,""view_old_events"":true,""view_new_events"":true}},""start_date"":""2017-02-14 09:45:52.768950"",""end_date"":null}}],""Pagination"":{""future_url"":""\/v1\/user\/736\/share-invite-bank-response?newer_id=266"",""newer_url"":null,""older_url"":null}}";
json = "{\"Response\":[{\"CredentialPasswordIp\":{\"id\":1131,\"created\":\"2017-02-14 18:47:13.227959\",\"updated\":\"2017-02-16 14:08:25.189684\",\"status\":\"ACTIVE\",\"expiry_time\":null,\"token_value\":null,\"permitted_device\":{\"description\":\"KIBServer20170215092040\",\"ip\":\"52.166.19.241\"}}},{\"CredentialPasswordIp\":{\"id\":1130,\"created\":\"2017-02-14 18:47:13.064456\",\"updated\":\"2017-02-15 13:31:46.370321\",\"status\":\"ACTIVE\",\"expiry_time\":null,\"token_value\":null,\"permitted_device\":{\"description\":\"Generated device\",\"ip\":\"10.8.0.51\"}}}],\"Pagination\":{\"future_url\":\"\\/v1\\/user\\/738\\/credential-password-ip?newer_id=1131\",\"newer_url\":null,\"older_url\":null}}" ;
JObject jObj = JObject.Parse(json);

//"$.Manufacturers[?(@.Name == 'Acme Co')]"
//jObj.SelectToken("$.Response[?(@.Id)].Id.id").Dump("Json query");

//var y = jObj.SelectToken("$.Response");
var y = jObj["Response"];

if (y == null)
	return;

foreach (var z in y)
{
	JToken shareinvite = z["CredentialPasswordIp"];
	//shareinvite.Dump();
	shareinvite["id"].Dump();
	shareinvite["permitted_device"]["description"].Dump();
	shareinvite["permitted_device"]["ip"].Dump();
//	shareinvite["counter_alias"]["iban"].Dump();
}
//jObj["Error"].First()["error_description"].Dump();