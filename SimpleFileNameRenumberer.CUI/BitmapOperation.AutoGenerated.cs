using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;

namespace SimpleFileNameRenumberer.CUI
{
    internal static partial class BitmapOperation
    {
        #region IsMonochromeImageAligned

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe bool IsMonochromeImageAligned(uint* buffer, uint elementLength)
        {
            var pixelData = buffer[0];
            var ptr = &buffer[1];
            var elementCount = elementLength - 1;
            if (Vector512.IsHardwareAccelerated && Vector512<uint>.IsSupported)
            {
                var v = Vector512.Create(pixelData);
                while (elementCount >= (uint)Vector512<uint>.Count * 32u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 0u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 1u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 2u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 3u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 4u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 5u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 6u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 7u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 8u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 9u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 10u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 11u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 12u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 13u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 14u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 15u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 16u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 17u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 18u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 19u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 20u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 21u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 22u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 23u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 24u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 25u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 26u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 27u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 28u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 29u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 30u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 31u), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 32u;
                    elementCount -= (uint)Vector512<uint>.Count * 32u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 16u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 0u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 1u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 2u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 3u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 4u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 5u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 6u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 7u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 8u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 9u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 10u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 11u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 12u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 13u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 14u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 15u), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 16u;
                    elementCount -= (uint)Vector512<uint>.Count * 16u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 8u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 0u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 1u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 2u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 3u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 4u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 5u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 6u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 7u), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 8u;
                    elementCount -= (uint)Vector512<uint>.Count * 8u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 4u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 0u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 1u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 2u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 3u), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 4u;
                    elementCount -= (uint)Vector512<uint>.Count * 4u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 2u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 0u), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 1u), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 2u;
                    elementCount -= (uint)Vector512<uint>.Count * 2u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 1u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load(ptr + (uint)Vector512<uint>.Count * 0u), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 1u;
                    elementCount -= (uint)Vector512<uint>.Count * 1u;
                }

                if (elementCount > 0)
                {
                    if (elementLength >= (uint)Vector512<uint>.Count)
                    {
                        if (!Vector512.EqualsAll(Vector512.Load(buffer + elementLength - (uint)Vector512<uint>.Count), v))
                            return false;
                    }
                    else
                    {
                        do
                        {
                            if (*ptr != pixelData)
                                return false;
                            ++ptr;
                            --elementCount;
                        } while (elementCount > 0);
                    }
                }

                return true;
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<uint>.IsSupported)
            {
                var v = Vector256.Create(pixelData);
                while (elementCount >= (uint)Vector256<uint>.Count * 32u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 0u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 1u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 2u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 3u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 4u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 5u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 6u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 7u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 8u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 9u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 10u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 11u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 12u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 13u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 14u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 15u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 16u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 17u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 18u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 19u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 20u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 21u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 22u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 23u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 24u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 25u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 26u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 27u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 28u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 29u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 30u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 31u), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 32u;
                    elementCount -= (uint)Vector256<uint>.Count * 32u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 16u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 0u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 1u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 2u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 3u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 4u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 5u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 6u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 7u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 8u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 9u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 10u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 11u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 12u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 13u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 14u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 15u), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 16u;
                    elementCount -= (uint)Vector256<uint>.Count * 16u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 8u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 0u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 1u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 2u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 3u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 4u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 5u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 6u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 7u), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 8u;
                    elementCount -= (uint)Vector256<uint>.Count * 8u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 4u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 0u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 1u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 2u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 3u), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 4u;
                    elementCount -= (uint)Vector256<uint>.Count * 4u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 2u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 0u), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 1u), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 2u;
                    elementCount -= (uint)Vector256<uint>.Count * 2u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 1u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load(ptr + (uint)Vector256<uint>.Count * 0u), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 1u;
                    elementCount -= (uint)Vector256<uint>.Count * 1u;
                }

                if (elementCount > 0)
                {
                    if (elementLength >= (uint)Vector256<uint>.Count)
                    {
                        if (!Vector256.EqualsAll(Vector256.Load(buffer + elementLength - (uint)Vector256<uint>.Count), v))
                            return false;
                    }
                    else
                    {
                        do
                        {
                            if (*ptr != pixelData)
                                return false;
                            ++ptr;
                            --elementCount;
                        } while (elementCount > 0);
                    }
                }

                return true;
            }
            else if (Vector128.IsHardwareAccelerated && Vector128<uint>.IsSupported)
            {
                var v = Vector128.Create(pixelData);
                while (elementCount >= (uint)Vector128<uint>.Count * 32u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 0u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 1u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 2u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 3u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 4u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 5u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 6u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 7u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 8u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 9u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 10u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 11u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 12u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 13u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 14u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 15u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 16u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 17u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 18u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 19u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 20u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 21u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 22u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 23u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 24u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 25u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 26u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 27u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 28u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 29u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 30u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 31u), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 32u;
                    elementCount -= (uint)Vector128<uint>.Count * 32u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 16u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 0u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 1u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 2u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 3u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 4u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 5u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 6u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 7u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 8u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 9u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 10u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 11u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 12u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 13u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 14u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 15u), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 16u;
                    elementCount -= (uint)Vector128<uint>.Count * 16u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 8u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 0u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 1u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 2u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 3u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 4u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 5u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 6u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 7u), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 8u;
                    elementCount -= (uint)Vector128<uint>.Count * 8u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 4u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 0u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 1u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 2u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 3u), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 4u;
                    elementCount -= (uint)Vector128<uint>.Count * 4u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 2u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 0u), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 1u), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 2u;
                    elementCount -= (uint)Vector128<uint>.Count * 2u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 1u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load(ptr + (uint)Vector128<uint>.Count * 0u), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 1u;
                    elementCount -= (uint)Vector128<uint>.Count * 1u;
                }

                if (elementCount > 0)
                {
                    if (elementLength >= (uint)Vector128<uint>.Count)
                    {
                        if (!Vector128.EqualsAll(Vector128.Load(buffer + elementLength - (uint)Vector128<uint>.Count), v))
                            return false;
                    }
                    else
                    {
                        do
                        {
                            if (*ptr != pixelData)
                                return false;
                            ++ptr;
                            --elementCount;
                        } while (elementCount > 0);
                    }
                }

                return true;
            }
            else
            {
                while (elementCount >= 32u)
                {
                    if (ptr[0] != pixelData)
                        return false;
                    if (ptr[1] != pixelData)
                        return false;
                    if (ptr[2] != pixelData)
                        return false;
                    if (ptr[3] != pixelData)
                        return false;
                    if (ptr[4] != pixelData)
                        return false;
                    if (ptr[5] != pixelData)
                        return false;
                    if (ptr[6] != pixelData)
                        return false;
                    if (ptr[7] != pixelData)
                        return false;
                    if (ptr[8] != pixelData)
                        return false;
                    if (ptr[9] != pixelData)
                        return false;
                    if (ptr[10] != pixelData)
                        return false;
                    if (ptr[11] != pixelData)
                        return false;
                    if (ptr[12] != pixelData)
                        return false;
                    if (ptr[13] != pixelData)
                        return false;
                    if (ptr[14] != pixelData)
                        return false;
                    if (ptr[15] != pixelData)
                        return false;
                    if (ptr[16] != pixelData)
                        return false;
                    if (ptr[17] != pixelData)
                        return false;
                    if (ptr[18] != pixelData)
                        return false;
                    if (ptr[19] != pixelData)
                        return false;
                    if (ptr[20] != pixelData)
                        return false;
                    if (ptr[21] != pixelData)
                        return false;
                    if (ptr[22] != pixelData)
                        return false;
                    if (ptr[23] != pixelData)
                        return false;
                    if (ptr[24] != pixelData)
                        return false;
                    if (ptr[25] != pixelData)
                        return false;
                    if (ptr[26] != pixelData)
                        return false;
                    if (ptr[27] != pixelData)
                        return false;
                    if (ptr[28] != pixelData)
                        return false;
                    if (ptr[29] != pixelData)
                        return false;
                    if (ptr[30] != pixelData)
                        return false;
                    if (ptr[31] != pixelData)
                        return false;
                    ptr += 32u;
                    elementCount -= 32u;
                }

                if (elementCount >= 16u)
                {
                    if (ptr[0] != pixelData)
                        return false;
                    if (ptr[1] != pixelData)
                        return false;
                    if (ptr[2] != pixelData)
                        return false;
                    if (ptr[3] != pixelData)
                        return false;
                    if (ptr[4] != pixelData)
                        return false;
                    if (ptr[5] != pixelData)
                        return false;
                    if (ptr[6] != pixelData)
                        return false;
                    if (ptr[7] != pixelData)
                        return false;
                    if (ptr[8] != pixelData)
                        return false;
                    if (ptr[9] != pixelData)
                        return false;
                    if (ptr[10] != pixelData)
                        return false;
                    if (ptr[11] != pixelData)
                        return false;
                    if (ptr[12] != pixelData)
                        return false;
                    if (ptr[13] != pixelData)
                        return false;
                    if (ptr[14] != pixelData)
                        return false;
                    if (ptr[15] != pixelData)
                        return false;
                    ptr += 16u;
                    elementCount -= 16u;
                }

                if (elementCount >= 8u)
                {
                    if (ptr[0] != pixelData)
                        return false;
                    if (ptr[1] != pixelData)
                        return false;
                    if (ptr[2] != pixelData)
                        return false;
                    if (ptr[3] != pixelData)
                        return false;
                    if (ptr[4] != pixelData)
                        return false;
                    if (ptr[5] != pixelData)
                        return false;
                    if (ptr[6] != pixelData)
                        return false;
                    if (ptr[7] != pixelData)
                        return false;
                    ptr += 8u;
                    elementCount -= 8u;
                }

                if (elementCount >= 4u)
                {
                    if (ptr[0] != pixelData)
                        return false;
                    if (ptr[1] != pixelData)
                        return false;
                    if (ptr[2] != pixelData)
                        return false;
                    if (ptr[3] != pixelData)
                        return false;
                    ptr += 4u;
                    elementCount -= 4u;
                }

                if (elementCount >= 2u)
                {
                    if (ptr[0] != pixelData)
                        return false;
                    if (ptr[1] != pixelData)
                        return false;
                    ptr += 2u;
                    elementCount -= 2u;
                }

                if (elementCount > 0)
                {
                    if (ptr[0] != pixelData)
                        return false;
                }

                return true;
            }
        }

        #endregion

        #region IsMonochromeImage

        [MethodImpl(MethodImplOptions.AggressiveInlining | MethodImplOptions.AggressiveOptimization)]
        public static unsafe bool IsMonochromeImage(byte* buffer, uint elementLength)
        {
            var pixelData = ((uint)buffer[0] << (8 * 0)) | ((uint)buffer[1] << (8 * 1)) | ((uint)buffer[2] << (8 * 2)) | ((uint)buffer[3] << (8 * 3));
            var ptr = &buffer[4];
            var elementCount = elementLength - 1;
            if (Vector512.IsHardwareAccelerated && Vector512<uint>.IsSupported)
            {
                var v = Vector512.Create(pixelData);
                while (elementCount >= (uint)Vector512<uint>.Count * 32u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 28u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 32u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 36u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 40u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 44u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 48u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 52u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 56u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 60u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 64u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 68u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 72u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 76u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 80u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 84u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 88u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 92u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 96u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 100u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 104u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 108u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 112u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 116u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 120u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 124u)), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 128u;
                    elementCount -= (uint)Vector512<uint>.Count * 32u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 16u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 28u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 32u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 36u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 40u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 44u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 48u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 52u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 56u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 60u)), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 64u;
                    elementCount -= (uint)Vector512<uint>.Count * 16u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 8u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 28u)), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 32u;
                    elementCount -= (uint)Vector512<uint>.Count * 8u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 4u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 12u)), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 16u;
                    elementCount -= (uint)Vector512<uint>.Count * 4u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 2u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 4u)), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 8u;
                    elementCount -= (uint)Vector512<uint>.Count * 2u;
                }

                if (elementCount >= (uint)Vector512<uint>.Count * 1u)
                {
                    if (!Vector512.EqualsAll(Vector512.Load((uint*)(ptr + (uint)Vector512<uint>.Count * 0u)), v))
                        return false;
                    ptr += (uint)Vector512<uint>.Count * 4u;
                    elementCount -= (uint)Vector512<uint>.Count * 1u;
                }

                if (elementCount > 0)
                {
                    if (elementLength >= (uint)Vector512<uint>.Count)
                    {
                        if (!Vector512.EqualsAll(Vector512.Load((uint*)(buffer + sizeof(uint) * (elementLength - (uint)Vector512<uint>.Count))), v))
                            return false;
                    }
                    else
                    {
                        do
                        {
                            if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                                return false;
                            ptr += sizeof(uint);
                            --elementCount;
                        } while (elementCount > 0);
                    }
                }

                return true;
            }
            else if (Vector256.IsHardwareAccelerated && Vector256<uint>.IsSupported)
            {
                var v = Vector256.Create(pixelData);
                while (elementCount >= (uint)Vector256<uint>.Count * 32u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 28u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 32u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 36u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 40u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 44u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 48u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 52u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 56u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 60u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 64u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 68u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 72u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 76u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 80u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 84u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 88u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 92u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 96u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 100u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 104u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 108u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 112u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 116u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 120u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 124u)), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 128u;
                    elementCount -= (uint)Vector256<uint>.Count * 32u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 16u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 28u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 32u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 36u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 40u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 44u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 48u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 52u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 56u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 60u)), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 64u;
                    elementCount -= (uint)Vector256<uint>.Count * 16u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 8u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 28u)), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 32u;
                    elementCount -= (uint)Vector256<uint>.Count * 8u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 4u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 12u)), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 16u;
                    elementCount -= (uint)Vector256<uint>.Count * 4u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 2u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 4u)), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 8u;
                    elementCount -= (uint)Vector256<uint>.Count * 2u;
                }

                if (elementCount >= (uint)Vector256<uint>.Count * 1u)
                {
                    if (!Vector256.EqualsAll(Vector256.Load((uint*)(ptr + (uint)Vector256<uint>.Count * 0u)), v))
                        return false;
                    ptr += (uint)Vector256<uint>.Count * 4u;
                    elementCount -= (uint)Vector256<uint>.Count * 1u;
                }

                if (elementCount > 0)
                {
                    if (elementLength >= (uint)Vector256<uint>.Count)
                    {
                        if (!Vector256.EqualsAll(Vector256.Load((uint*)(buffer + sizeof(uint) * (elementLength - (uint)Vector256<uint>.Count))), v))
                            return false;
                    }
                    else
                    {
                        do
                        {
                            if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                                return false;
                            ptr += sizeof(uint);
                            --elementCount;
                        } while (elementCount > 0);
                    }
                }

                return true;
            }
            else if (Vector128.IsHardwareAccelerated && Vector128<uint>.IsSupported)
            {
                var v = Vector128.Create(pixelData);
                while (elementCount >= (uint)Vector128<uint>.Count * 32u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 28u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 32u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 36u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 40u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 44u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 48u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 52u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 56u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 60u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 64u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 68u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 72u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 76u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 80u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 84u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 88u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 92u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 96u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 100u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 104u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 108u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 112u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 116u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 120u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 124u)), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 128u;
                    elementCount -= (uint)Vector128<uint>.Count * 32u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 16u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 28u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 32u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 36u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 40u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 44u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 48u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 52u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 56u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 60u)), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 64u;
                    elementCount -= (uint)Vector128<uint>.Count * 16u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 8u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 12u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 16u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 20u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 24u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 28u)), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 32u;
                    elementCount -= (uint)Vector128<uint>.Count * 8u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 4u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 4u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 8u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 12u)), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 16u;
                    elementCount -= (uint)Vector128<uint>.Count * 4u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 2u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 0u)), v))
                        return false;
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 4u)), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 8u;
                    elementCount -= (uint)Vector128<uint>.Count * 2u;
                }

                if (elementCount >= (uint)Vector128<uint>.Count * 1u)
                {
                    if (!Vector128.EqualsAll(Vector128.Load((uint*)(ptr + (uint)Vector128<uint>.Count * 0u)), v))
                        return false;
                    ptr += (uint)Vector128<uint>.Count * 4u;
                    elementCount -= (uint)Vector128<uint>.Count * 1u;
                }

                if (elementCount > 0)
                {
                    if (elementLength >= (uint)Vector128<uint>.Count)
                    {
                        if (!Vector128.EqualsAll(Vector128.Load((uint*)(buffer + sizeof(uint) * (elementLength - (uint)Vector128<uint>.Count))), v))
                            return false;
                    }
                    else
                    {
                        do
                        {
                            if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                                return false;
                            ptr += sizeof(uint);
                            --elementCount;
                        } while (elementCount > 0);
                    }
                }

                return true;
            }
            else
            {
                while (elementCount >= 32u)
                {
                    if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[4] << (8 * 0)) | ((uint)ptr[5] << (8 * 1)) | ((uint)ptr[6] << (8 * 2)) | ((uint)ptr[7] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[8] << (8 * 0)) | ((uint)ptr[9] << (8 * 1)) | ((uint)ptr[10] << (8 * 2)) | ((uint)ptr[11] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[12] << (8 * 0)) | ((uint)ptr[13] << (8 * 1)) | ((uint)ptr[14] << (8 * 2)) | ((uint)ptr[15] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[16] << (8 * 0)) | ((uint)ptr[17] << (8 * 1)) | ((uint)ptr[18] << (8 * 2)) | ((uint)ptr[19] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[20] << (8 * 0)) | ((uint)ptr[21] << (8 * 1)) | ((uint)ptr[22] << (8 * 2)) | ((uint)ptr[23] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[24] << (8 * 0)) | ((uint)ptr[25] << (8 * 1)) | ((uint)ptr[26] << (8 * 2)) | ((uint)ptr[27] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[28] << (8 * 0)) | ((uint)ptr[29] << (8 * 1)) | ((uint)ptr[30] << (8 * 2)) | ((uint)ptr[31] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[32] << (8 * 0)) | ((uint)ptr[33] << (8 * 1)) | ((uint)ptr[34] << (8 * 2)) | ((uint)ptr[35] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[36] << (8 * 0)) | ((uint)ptr[37] << (8 * 1)) | ((uint)ptr[38] << (8 * 2)) | ((uint)ptr[39] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[40] << (8 * 0)) | ((uint)ptr[41] << (8 * 1)) | ((uint)ptr[42] << (8 * 2)) | ((uint)ptr[43] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[44] << (8 * 0)) | ((uint)ptr[45] << (8 * 1)) | ((uint)ptr[46] << (8 * 2)) | ((uint)ptr[47] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[48] << (8 * 0)) | ((uint)ptr[49] << (8 * 1)) | ((uint)ptr[50] << (8 * 2)) | ((uint)ptr[51] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[52] << (8 * 0)) | ((uint)ptr[53] << (8 * 1)) | ((uint)ptr[54] << (8 * 2)) | ((uint)ptr[55] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[56] << (8 * 0)) | ((uint)ptr[57] << (8 * 1)) | ((uint)ptr[58] << (8 * 2)) | ((uint)ptr[59] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[60] << (8 * 0)) | ((uint)ptr[61] << (8 * 1)) | ((uint)ptr[62] << (8 * 2)) | ((uint)ptr[63] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[64] << (8 * 0)) | ((uint)ptr[65] << (8 * 1)) | ((uint)ptr[66] << (8 * 2)) | ((uint)ptr[67] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[68] << (8 * 0)) | ((uint)ptr[69] << (8 * 1)) | ((uint)ptr[70] << (8 * 2)) | ((uint)ptr[71] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[72] << (8 * 0)) | ((uint)ptr[73] << (8 * 1)) | ((uint)ptr[74] << (8 * 2)) | ((uint)ptr[75] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[76] << (8 * 0)) | ((uint)ptr[77] << (8 * 1)) | ((uint)ptr[78] << (8 * 2)) | ((uint)ptr[79] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[80] << (8 * 0)) | ((uint)ptr[81] << (8 * 1)) | ((uint)ptr[82] << (8 * 2)) | ((uint)ptr[83] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[84] << (8 * 0)) | ((uint)ptr[85] << (8 * 1)) | ((uint)ptr[86] << (8 * 2)) | ((uint)ptr[87] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[88] << (8 * 0)) | ((uint)ptr[89] << (8 * 1)) | ((uint)ptr[90] << (8 * 2)) | ((uint)ptr[91] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[92] << (8 * 0)) | ((uint)ptr[93] << (8 * 1)) | ((uint)ptr[94] << (8 * 2)) | ((uint)ptr[95] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[96] << (8 * 0)) | ((uint)ptr[97] << (8 * 1)) | ((uint)ptr[98] << (8 * 2)) | ((uint)ptr[99] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[100] << (8 * 0)) | ((uint)ptr[101] << (8 * 1)) | ((uint)ptr[102] << (8 * 2)) | ((uint)ptr[103] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[104] << (8 * 0)) | ((uint)ptr[105] << (8 * 1)) | ((uint)ptr[106] << (8 * 2)) | ((uint)ptr[107] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[108] << (8 * 0)) | ((uint)ptr[109] << (8 * 1)) | ((uint)ptr[110] << (8 * 2)) | ((uint)ptr[111] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[112] << (8 * 0)) | ((uint)ptr[113] << (8 * 1)) | ((uint)ptr[114] << (8 * 2)) | ((uint)ptr[115] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[116] << (8 * 0)) | ((uint)ptr[117] << (8 * 1)) | ((uint)ptr[118] << (8 * 2)) | ((uint)ptr[119] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[120] << (8 * 0)) | ((uint)ptr[121] << (8 * 1)) | ((uint)ptr[122] << (8 * 2)) | ((uint)ptr[123] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[124] << (8 * 0)) | ((uint)ptr[125] << (8 * 1)) | ((uint)ptr[126] << (8 * 2)) | ((uint)ptr[127] << (8 * 3))) != pixelData)
                        return false;
                    ptr += 128u;
                    elementCount -= 32u;
                }

                if (elementCount >= 16u)
                {
                    if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[4] << (8 * 0)) | ((uint)ptr[5] << (8 * 1)) | ((uint)ptr[6] << (8 * 2)) | ((uint)ptr[7] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[8] << (8 * 0)) | ((uint)ptr[9] << (8 * 1)) | ((uint)ptr[10] << (8 * 2)) | ((uint)ptr[11] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[12] << (8 * 0)) | ((uint)ptr[13] << (8 * 1)) | ((uint)ptr[14] << (8 * 2)) | ((uint)ptr[15] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[16] << (8 * 0)) | ((uint)ptr[17] << (8 * 1)) | ((uint)ptr[18] << (8 * 2)) | ((uint)ptr[19] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[20] << (8 * 0)) | ((uint)ptr[21] << (8 * 1)) | ((uint)ptr[22] << (8 * 2)) | ((uint)ptr[23] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[24] << (8 * 0)) | ((uint)ptr[25] << (8 * 1)) | ((uint)ptr[26] << (8 * 2)) | ((uint)ptr[27] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[28] << (8 * 0)) | ((uint)ptr[29] << (8 * 1)) | ((uint)ptr[30] << (8 * 2)) | ((uint)ptr[31] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[32] << (8 * 0)) | ((uint)ptr[33] << (8 * 1)) | ((uint)ptr[34] << (8 * 2)) | ((uint)ptr[35] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[36] << (8 * 0)) | ((uint)ptr[37] << (8 * 1)) | ((uint)ptr[38] << (8 * 2)) | ((uint)ptr[39] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[40] << (8 * 0)) | ((uint)ptr[41] << (8 * 1)) | ((uint)ptr[42] << (8 * 2)) | ((uint)ptr[43] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[44] << (8 * 0)) | ((uint)ptr[45] << (8 * 1)) | ((uint)ptr[46] << (8 * 2)) | ((uint)ptr[47] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[48] << (8 * 0)) | ((uint)ptr[49] << (8 * 1)) | ((uint)ptr[50] << (8 * 2)) | ((uint)ptr[51] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[52] << (8 * 0)) | ((uint)ptr[53] << (8 * 1)) | ((uint)ptr[54] << (8 * 2)) | ((uint)ptr[55] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[56] << (8 * 0)) | ((uint)ptr[57] << (8 * 1)) | ((uint)ptr[58] << (8 * 2)) | ((uint)ptr[59] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[60] << (8 * 0)) | ((uint)ptr[61] << (8 * 1)) | ((uint)ptr[62] << (8 * 2)) | ((uint)ptr[63] << (8 * 3))) != pixelData)
                        return false;
                    ptr += 64u;
                    elementCount -= 16u;
                }

                if (elementCount >= 8u)
                {
                    if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[4] << (8 * 0)) | ((uint)ptr[5] << (8 * 1)) | ((uint)ptr[6] << (8 * 2)) | ((uint)ptr[7] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[8] << (8 * 0)) | ((uint)ptr[9] << (8 * 1)) | ((uint)ptr[10] << (8 * 2)) | ((uint)ptr[11] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[12] << (8 * 0)) | ((uint)ptr[13] << (8 * 1)) | ((uint)ptr[14] << (8 * 2)) | ((uint)ptr[15] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[16] << (8 * 0)) | ((uint)ptr[17] << (8 * 1)) | ((uint)ptr[18] << (8 * 2)) | ((uint)ptr[19] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[20] << (8 * 0)) | ((uint)ptr[21] << (8 * 1)) | ((uint)ptr[22] << (8 * 2)) | ((uint)ptr[23] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[24] << (8 * 0)) | ((uint)ptr[25] << (8 * 1)) | ((uint)ptr[26] << (8 * 2)) | ((uint)ptr[27] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[28] << (8 * 0)) | ((uint)ptr[29] << (8 * 1)) | ((uint)ptr[30] << (8 * 2)) | ((uint)ptr[31] << (8 * 3))) != pixelData)
                        return false;
                    ptr += 32u;
                    elementCount -= 8u;
                }

                if (elementCount >= 4u)
                {
                    if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[4] << (8 * 0)) | ((uint)ptr[5] << (8 * 1)) | ((uint)ptr[6] << (8 * 2)) | ((uint)ptr[7] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[8] << (8 * 0)) | ((uint)ptr[9] << (8 * 1)) | ((uint)ptr[10] << (8 * 2)) | ((uint)ptr[11] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[12] << (8 * 0)) | ((uint)ptr[13] << (8 * 1)) | ((uint)ptr[14] << (8 * 2)) | ((uint)ptr[15] << (8 * 3))) != pixelData)
                        return false;
                    ptr += 16u;
                    elementCount -= 4u;
                }

                if (elementCount >= 2u)
                {
                    if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                        return false;
                    if ((((uint)ptr[4] << (8 * 0)) | ((uint)ptr[5] << (8 * 1)) | ((uint)ptr[6] << (8 * 2)) | ((uint)ptr[7] << (8 * 3))) != pixelData)
                        return false;
                    ptr += 8u;
                    elementCount -= 2u;
                }

                if (elementCount > 0)
                {
                    if ((((uint)ptr[0] << (8 * 0)) | ((uint)ptr[1] << (8 * 1)) | ((uint)ptr[2] << (8 * 2)) | ((uint)ptr[3] << (8 * 3))) != pixelData)
                        return false;
                }

                return true;
            }
        }

        #endregion
    }
}
