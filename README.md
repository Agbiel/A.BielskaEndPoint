Content Parser API (.NET 10)
Punkt końcowy (API endpoint) w technologii ASP.NET, służący do dekodowania z Base64 i parsowania danych w formatach CSV oraz INTERNAL_JSON.

Uruchomienie
Wymagany .NET 8 SDK (najlepiej najnowszy 10).

Uruchom aplikację:

Bash
dotnet run

Specyfikacja API
Endpoint: POST /api/v1/parse-content

Header: Content-Type: application/json

Payload wejściowy
JSON
{
  "type": "CSV",
  "content": "SmFuLEtvd2Fsc2tpLDI1"
}
type: "CSV" lub "INTERNAL_JSON"

content: Dane zakodowane w Base64

Przykłady odpowiedzi (HTTP 200)
Dla CSV:

JSON
{
  "success": true,
  "count": 1,
  "data": [
    { "values": ["Agnieszka", "Bielska", "29"] }
  ]
}
Dla INTERNAL_JSON:

JSON
{
  "success": true,
  "count": 1,
  "data": [
    { "id": 1, "name": "Produkt A" }
  ]
}
Błędy (HTTP 400)
Aplikacja zwraca 400 Bad Request w przypadku: niepoprawnego Base64, pustego pola content lub nieobsługiwanego type.

Klonowanie repozytorium
bash
git clone https://github.com/Agbiel/A.BielskaEndPoint.git
