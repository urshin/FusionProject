using System;

/// <summary>
/// Fusion ���� ��ū ��ƿ��Ƽ �޼���
/// </summary>
public static class ConnectionTokenUtils
{
    /// <summary>
    /// ���ο� ���� ��ū ����
    /// </summary>
    public static byte[] NewToken() => Guid.NewGuid().ToByteArray();

    /// <summary>
    /// ��ū�� �ؽ� �������� ��ȯ
    /// </summary>
    /// <param name="token">�ؽ��� ��ū</param>
    /// <returns>��ū �ؽ�</returns>
    public static int HashToken(byte[] token) => new Guid(token).GetHashCode();

    /// <summary>
    /// ��ū�� ���ڿ��� ��ȯ
    /// </summary>
    /// <param name="token">��ȯ�� ��ū</param>
    /// <returns>���ڿ��� �� ��ū</returns>
    public static string TokenToString(byte[] token) => new Guid(token).ToString();
}