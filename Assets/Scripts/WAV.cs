using UnityEngine;
using System.Collections;
using System.IO;
using System.Text;
using System;

public class WAV
{
    public static byte[] WriteWavHeader(bool isFloatingPoint, ushort channelCount, ushort bitDepth, int sampleRate, int totalSampleCount)
    {
        byte[] buffer = new byte[44];

        // RIFF header.
        // Chunk ID.
        buffer[0] = Encoding.ASCII.GetBytes("R")[0];
        buffer[1] = Encoding.ASCII.GetBytes("I")[0];
        buffer[2] = Encoding.ASCII.GetBytes("F")[0];
        buffer[3] = Encoding.ASCII.GetBytes("F")[0];

        // Chunk size.
        byte[] temp = BitConverter.GetBytes(((bitDepth / 8) * totalSampleCount) + 36);
        for (int i = 0; i < 4; i++)
        {
            buffer[4 + i] = temp[i];
        }

        // Format.
        buffer[8] = Encoding.ASCII.GetBytes("W")[0];
        buffer[9] = Encoding.ASCII.GetBytes("A")[0];
        buffer[10] = Encoding.ASCII.GetBytes("V")[0];
        buffer[11] = Encoding.ASCII.GetBytes("E")[0];

        // Sub-chunk 1.
        // Sub-chunk 1 ID.
        buffer[12] = Encoding.ASCII.GetBytes("f")[0];
        buffer[13] = Encoding.ASCII.GetBytes("m")[0];
        buffer[14] = Encoding.ASCII.GetBytes("t")[0];
        buffer[15] = Encoding.ASCII.GetBytes(" ")[0];

        // Sub-chunk 1 size.
        temp = BitConverter.GetBytes(16);
        for (int i = 0; i < 4; i++)
        {
            buffer[16 + i] = temp[i];
        }

        // Audio format (floating point (3) or PCM (1)). Any other format indicates compression.
        temp = BitConverter.GetBytes((ushort)(isFloatingPoint ? 3 : 1));
        for (int i = 0; i < 2; i++)
        {
            buffer[20 + i] = temp[i];
        }

        // Channels.
        temp = BitConverter.GetBytes(channelCount);
        for (int i = 0; i < 2; i++)
        {
            buffer[22 + i] = temp[i];
        }

        // Sample rate.
        temp = BitConverter.GetBytes(sampleRate);
        for (int i = 0; i < 4; i++)
        {
            buffer[24 + i] = temp[i];
        }

        // Bytes rate.
        temp = BitConverter.GetBytes(sampleRate * channelCount * (bitDepth / 8));
        for (int i = 0; i < 4; i++)
        {
            buffer[28 + i] = temp[i];
        }

        // Block align.
        temp = BitConverter.GetBytes((ushort)channelCount * (bitDepth / 8));
        for (int i = 0; i < 2; i++)
        {
            buffer[32 + i] = temp[i];
        }

        // Bits per sample.
        temp = BitConverter.GetBytes(bitDepth);
        for (int i = 0; i < 2; i++)
        {
            buffer[34 + i] = temp[i];
        }

        // Sub-chunk 2.
        // Sub-chunk 2 ID.
        temp = Encoding.ASCII.GetBytes("data");
        for (int i = 0; i < 4; i++)
        {
            buffer[36 + i] = temp[i];
        }

        // Sub-chunk 2 size.
        temp = BitConverter.GetBytes((bitDepth / 8) * totalSampleCount);
        for (int i = 0; i < 4; i++)
        {
            buffer[40 + i] = temp[i];
        }

        return buffer;
    }
}