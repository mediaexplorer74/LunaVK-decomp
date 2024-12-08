// Decompiled with JetBrains decompiler
// Type: App1uwp.Network.ResultCode
// Assembly: App1uwp.WindowsPhone, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: B8839920-99B5-46F4-911B-11E72A9783F1
// Assembly location: C:\Users\Admin\Desktop\RE\LunaVK\App1uwp.WindowsPhone_0.0.1.32_AnyCPU\App1uwp.WindowsPhone.exe

#nullable disable
namespace App1uwp.Network
{
  public enum ResultCode
  {
    DeserializationError = -10000, // 0xFFFFD8F0
    UploadingFailed = -2, // 0xFFFFFFFE
    CommunicationFailed = -1, // 0xFFFFFFFF
    Succeeded = 0,
    UnknownError = 1,
    AppDisabled = 2,
    UnknownMethod = 3,
    IncorrectSignature = 4,
    UserAuthorizationFailed = 5,
    TooManyRequestsPerSecond = 6,
    NotAllowed = 7,
    FloodControlEnabled = 9,
    InternalServerError = 10, // 0x0000000A
    CaptchaRequired = 14, // 0x0000000E
    AccessDenied = 15, // 0x0000000F
    ValidationRequired = 17, // 0x00000011
    DeletedOrBanned = 18, // 0x00000012
    ConfirmationRequired = 24, // 0x00000018
    TokenConfirmationRequired = 25, // 0x00000019
    WrongParameter = 100, // 0x00000064
    OutOfLimits = 103, // 0x00000067
    InvalidUserIds = 113, // 0x00000071
    InvalidAudioFormat = 123, // 0x0000007B
    CannotAddYourself = 174, // 0x000000AE
    UserIsBlackListed = 175, // 0x000000AF
    PricavySettingsRestriction = 176, // 0x000000B0
    AccessDeniedExtended = 204, // 0x000000CC
    PostsLimitOrAlreadyScheduled = 214, // 0x000000D6
    AudioIsExcludedByRightholder = 270, // 0x0000010E
    AudioFileSizeLimitReached = 302, // 0x0000012E
    MaximumLimitReached = 302, // 0x0000012E
    Unauthorized = 401, // 0x00000191
    NotEnoughMoney = 504, // 0x000001F8
    WrongPhoneNumberFormat = 1000, // 0x000003E8
    UserAlreadyInvited = 1003, // 0x000003EB
    PhoneAlreadyRegistered = 1004, // 0x000003EC
    InvalidCode = 1110, // 0x00000456
    BadPassword = 1111, // 0x00000457
    Processing = 1112, // 0x00000458
    ProductNotFound = 1211, // 0x000004BB
    VideoNotFound = 1212, // 0x000004BC
    CatalogIsNotAvailable = 1310, // 0x0000051E
    CatalogCategoriesAreNotAvailable = 1311, // 0x0000051F
    WallIsDisabled = 10006, // 0x00002716
    NewLongPollServerRequested = 100000, // 0x000186A0
    WrongUsernameOrPassword = 100001, // 0x000186A1
    CaptchaControlCancelled = 100002, // 0x000186A2
    ValidationCancelledOrFailed = 100003, // 0x000186A3
    ConfirmationCancelled = 100004, // 0x000186A4
    BalanceRefillCancelled = 100005, // 0x000186A5
  }
}
