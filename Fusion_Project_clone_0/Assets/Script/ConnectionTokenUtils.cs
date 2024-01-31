using System;

/// <summary>
/// Fusion 연결 토큰 유틸리티 메서드
/// </summary>
public static class ConnectionTokenUtils
{
    /// <summary>
    /// 새로운 랜덤 토큰 생성
    /// </summary>
    public static byte[] NewToken() => Guid.NewGuid().ToByteArray();

    /// <summary>
    /// 토큰을 해시 형식으로 변환
    /// </summary>
    /// <param name="token">해싱할 토큰</param>
    /// <returns>토큰 해시</returns>
    public static int HashToken(byte[] token) => new Guid(token).GetHashCode();

    /// <summary>
    /// 토큰을 문자열로 변환
    /// </summary>
    /// <param name="token">변환할 토큰</param>
    /// <returns>문자열로 된 토큰</returns>
    public static string TokenToString(byte[] token) => new Guid(token).ToString();
}