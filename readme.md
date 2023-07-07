
# Blooper

I have been asked to create a simple chat app API which 'bloops' out words which are deemed 'sensitive'. I have created CRUD endpoints for internal use and one external use endpoint.


## Project Requirements


### CRUD endpoints (internal consumption)

I have created CRUD endpoints for the blooper words.

### Business logic endpoints (external consumption)

I have created one external endpoint, `/messages`.

### How this would be deployed

Assuming this would be an added feature on an existing program (as described in the brief) you could simply the `Blooper` method to the existing `Message` controller, as I have done in this project. You will also need to add another `POST` endpoint which implements this method, or you could simply alter the existing external `POST` endpoint by running `text` through the `Blooper` method.
## Run Locally

Use the following SQL query to create the required table:

```
CREATE TABLE bloopers (
	id int IDENTITY(1, 1) PRIMARY KEY,
	word NVARCHAR(255) NOT NULL
)
```

