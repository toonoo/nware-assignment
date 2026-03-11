# Authorization Guide

This API requires Bearer token authentication for all endpoints.

## Setup

### 1. API Keys Configuration

API Keys are configured in `appsettings.json`:

```json
{
  "Authorization": {
    "ApiKeys": [
      "nware-secret-key-123",
      "nware-secret-key-456",
      "nware-secret-key-789"
    ]
  }
}
```

For Development environment, use `appsettings.Development.json`.

### 2. Using the API

#### Via cURL
```bash
curl -X GET "http://localhost:5000/api/city/GetCities" \
  -H "Authorization: Bearer nware-secret-key-123"
```

#### Via Postman
1. Open Postman
2. Create a new request
3. Go to **Headers** tab
4. Add header: `Authorization: Bearer nware-secret-key-123`
5. Send the request

#### Via Swagger UI
1. Open Swagger UI at `http://localhost:5000/swagger`
2. Click the **Authorize** button (??)
3. Enter: `nware-secret-key-123`
4. Click **Authorize**
5. Test endpoints directly from Swagger

### 3. Request Examples

#### Get All Cities
```bash
curl -X GET "http://localhost:5000/api/city/GetCities" \
  -H "Authorization: Bearer nware-secret-key-123" \
  -H "Content-Type: application/json"
```

Response:
```json
{
  "code": 200,
  "message": "Success",
  "data": [
    {
      "City Name": "New York",
      "Country": "USA",
      "Population": 8335897
    }
  ],
  "total": 1
}
```

#### Search Cities by Name
```bash
curl -X GET "http://localhost:5000/api/city/GetCitiesByName?cityName=York" \
  -H "Authorization: Bearer nware-secret-key-123"
```

### 4. Error Responses

#### Missing Authorization Header
```json
{
  "code": 401,
  "message": "Authorization header is missing",
  "data": null
}
```

#### Invalid API Key
```json
{
  "code": 401,
  "message": "Invalid API Key",
  "data": null
}
```

## Security Best Practices

1. **Never commit API Keys in code**
   - Use environment variables or secrets manager
   - Keep production keys in `.gitignore`

2. **Use HTTPS in Production**
   - Always use Bearer tokens over HTTPS
   - Never use HTTP for authenticated requests

3. **Rotate Keys Regularly**
   - Change API keys periodically
   - Remove unused/old keys

4. **Use Environment-Specific Keys**
   - Different keys for Dev, Staging, Production
   - Use `appsettings.{Environment}.json`

## Available API Keys

### Development
- `dev-api-key-123`
- `dev-api-key-456`

### Production
- `nware-secret-key-123`
- `nware-secret-key-456`
- `nware-secret-key-789`

## Implementing in Your Application

### C# HttpClient Example
```csharp
using var client = new HttpClient();
client.DefaultRequestHeaders.Authorization = 
    new AuthenticationHeaderValue("Bearer", "nware-secret-key-123");

var response = await client.GetAsync("http://localhost:5000/api/city/GetCities");
```

### JavaScript Fetch Example
```javascript
const response = await fetch('http://localhost:5000/api/city/GetCities', {
  headers: {
    'Authorization': 'Bearer nware-secret-key-123'
  }
});
```

### Python Requests Example
```python
import requests

headers = {
    'Authorization': 'Bearer nware-secret-key-123'
}

response = requests.get('http://localhost:5000/api/city/GetCities', headers=headers)
```

## Future Enhancements

- Implement JWT (JSON Web Tokens) instead of simple API keys
- Add API Key expiration
- Implement Rate Limiting
- Add Role-Based Access Control (RBAC)
- Add API Key audit logging
