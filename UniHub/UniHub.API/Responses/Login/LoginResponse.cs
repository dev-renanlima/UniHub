namespace UniHub.API.Responses;

public record LoginResponse
(
    GetUserResponse User,
    string Token
);
  