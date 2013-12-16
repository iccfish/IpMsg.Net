/* 
  Copyright 2001-2009 Markus Hahn 
  All rights reserved. See documentation for license details.  
*/

using System;

namespace BlowfishNET
{

    /// <summary>Blowfish CBC implementation.</summary>
    /// <remarks>Use this class to encrypt or decrypt byte arrays or a single blocks
    /// with Blowfish in CBC (Cipher Block Block Chaining) mode. This is the recommended
    /// way to use Blowfish.NET, unless certain requirements (e.g. moving block without
    /// decryption) exist.
    /// </remarks>
    class BlowfishCBC : BlowfishECB
    {
        // we store the IV as two 32bit integers, to void packing and
        // unpacking inbetween the handling of data chunks
        uint ivHi;
        uint ivLo;
    
        /// <summary>The current initialization vector (IV), which measures one block.</summary>
        public byte[] IV
        {
            set 
            { 
                SetIV(value, 0);
            }

            get 
            { 
                byte[] result = new byte[BLOCK_SIZE];
                GetIV(result, 0);
                return result;
            }
        }

        /// <summary>Sets the initialization vector (IV) with an offset.</summary>
        /// <param name="buf">The buffer containing the new IV material.</param>
        /// <param name="ofs">Where the IV material starts.</param>
        public void SetIV(byte[] buf, int ofs)
        {
            this.ivHi = (((uint)buf[ofs    ]) << 24) |
                        (((uint)buf[ofs + 1]) << 16) |
                        (((uint)buf[ofs + 2]) <<  8) |
                                buf[ofs + 3];

            this.ivLo = (((uint)buf[ofs + 4]) << 24) |
                        (((uint)buf[ofs + 5]) << 16) |
                        (((uint)buf[ofs + 6]) <<  8) |
                                buf[ofs + 7];         
        }

        /// <summary>Gets the current IV material (of the size of one block).</summary>
        /// <param name="buf">The buffer to copy the IV to.</param>
        /// <param name="ofs">Where to start copying.</param>
        public void GetIV(byte[] buf, int ofs)
        {
            uint ivHi = this.ivHi;
            uint ivLo = this.ivLo;

            buf[ofs++] = (byte)(ivHi >> 24);   
            buf[ofs++] = (byte)(ivHi >> 16);   
            buf[ofs++] = (byte)(ivHi >>  8);   
            buf[ofs++] = (byte) ivHi;
    
            buf[ofs++] = (byte)(ivLo >> 24);   
            buf[ofs++] = (byte)(ivLo >> 16);   
            buf[ofs++] = (byte)(ivLo >>  8);   
            buf[ofs  ] = (byte) ivLo;              
        }

        /// <summary>Default constructor.</summary>
        /// <remarks>The IV needs to be assigned after the instance has been created!</remarks>
        /// <see cref="BlowfishNET.BlowfishECB.Initialize"/>
        public BlowfishCBC(byte[] key, int ofs, int len) : base(key, ofs, len)
        {
        }

        /// <summary>Zero key constructor.</summary>
        /// <remarks>After construction you need to initialize the instance and then apply the IV.</remarks>
        public BlowfishCBC() : base(null, 0, 0)
        {
        }

        /// <see cref="BlowfishNET.BlowfishECB.Invalidate"/>
        public new void Invalidate() 
        {
            base.Invalidate();
  
            this.ivHi = this.ivLo = 0;
        }

        /// <see cref="BlowfishNET.BlowfishECB.EncryptBlock"/>
        public new void EncryptBlock(
            uint hi,
            uint lo,
            out uint outHi,
            out uint outLo)
        {
            byte[] block = this.block;

            block[0] = (byte)(hi >> 24);  
            block[1] = (byte)(hi >> 16);  
            block[2] = (byte)(hi >>  8);  
            block[3] = (byte) hi; 
            block[4] = (byte)(lo >> 24);  
            block[5] = (byte)(lo >> 16);  
            block[6] = (byte)(lo >>  8);  
            block[7] = (byte) lo; 

            Encrypt(block, 0, block, 0, BLOCK_SIZE);

            outHi = (((uint)block[0]) << 24) |
                    (((uint)block[1]) << 16) |
                    (((uint)block[2]) <<  8) |
                            block[3];

            outLo = (((uint)block[4]) << 24) |
                    (((uint)block[5]) << 16) |
                    (((uint)block[6]) <<  8) |
                            block[7];
        }

        /// <see cref="BlowfishNET.BlowfishECB.DecryptBlock"/>
        public new void DecryptBlock(
            uint hi,
            uint lo,
            out uint outHi,
            out uint outLo)
        {
            byte[] block = this.block;
        
            block[0] = (byte)(hi >> 24);  
            block[1] = (byte)(hi >> 16);  
            block[2] = (byte)(hi >>  8);  
            block[3] = (byte) hi; 
            block[4] = (byte)(lo >> 24);  
            block[5] = (byte)(lo >> 16);  
            block[6] = (byte)(lo >>  8);  
            block[7] = (byte) lo; 

            Decrypt(block, 0, block, 0, BLOCK_SIZE);

            outHi = (((uint)block[0]) << 24) |
                    (((uint)block[1]) << 16) |
                    (((uint)block[2]) <<  8) |
                            block[3];

            outLo = (((uint)block[4]) << 24) |
                    (((uint)block[5]) << 16) |
                    (((uint)block[6]) <<  8) |
                            block[7];
        }

        /// <see cref="BlowfishNET.BlowfishECB.Encrypt"/>
        public new int Encrypt(
            byte[] dataIn,
            int posIn,
            byte[] dataOut,
            int posOut,
            int count) 
        {
            int end;
            
            uint[] sbox1 = this.sbox1;
            uint[] sbox2 = this.sbox2;
            uint[] sbox3 = this.sbox3;
            uint[] sbox4 = this.sbox4;

            uint[] pbox = this.pbox;

            uint pbox00 = pbox[ 0];
            uint pbox01 = pbox[ 1];
            uint pbox02 = pbox[ 2];
            uint pbox03 = pbox[ 3];
            uint pbox04 = pbox[ 4];
            uint pbox05 = pbox[ 5];
            uint pbox06 = pbox[ 6];
            uint pbox07 = pbox[ 7];
            uint pbox08 = pbox[ 8];
            uint pbox09 = pbox[ 9];
            uint pbox10 = pbox[10];
            uint pbox11 = pbox[11];
            uint pbox12 = pbox[12];
            uint pbox13 = pbox[13];
            uint pbox14 = pbox[14];
            uint pbox15 = pbox[15];
            uint pbox16 = pbox[16];
            uint pbox17 = pbox[17];

            uint hi = this.ivHi;
            uint lo = this.ivLo;

            count &= ~(BLOCK_SIZE - 1);

            end = posIn + count;

            while (posIn < end)
            {
                hi ^= (((uint)dataIn[posIn    ]) << 24) |
                      (((uint)dataIn[posIn + 1]) << 16) |
                      (((uint)dataIn[posIn + 2]) <<  8) |
                              dataIn[posIn + 3];
 
                lo ^= (((uint)dataIn[posIn + 4]) << 24) |
                      (((uint)dataIn[posIn + 5]) << 16) |
                      (((uint)dataIn[posIn + 6]) <<  8) |
                              dataIn[posIn + 7];

                posIn += 8; 

                hi ^= pbox00;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox01;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox02;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox03;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox04;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox05;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox06;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox07;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox08;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox09;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox10;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox11;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox12;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox13;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox14;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox15;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox16;

                uint swap = lo ^ pbox17;
                lo = hi;
                hi = swap;
                
                dataOut[posOut    ] = (byte)(hi >> 24);
                dataOut[posOut + 1] = (byte)(hi >> 16);
                dataOut[posOut + 2] = (byte)(hi >>  8);
                dataOut[posOut + 3] = (byte) hi;
                
                dataOut[posOut + 4] = (byte)(lo >> 24);
                dataOut[posOut + 5] = (byte)(lo >> 16);
                dataOut[posOut + 6] = (byte)(lo >>  8);
                dataOut[posOut + 7] = (byte) lo;

                posOut += 8;
            }

            this.ivHi = hi;
            this.ivLo = lo;

            return count;
        }

        /// <see cref="BlowfishNET.BlowfishECB.Decrypt"/>
        public new int Decrypt(
            byte[] dataIn,
            int posIn,
            byte[] dataOut,
            int posOut,
            int count) 
        {
            int end;
            uint hi, lo, hiBak, loBak;
            
            uint[] sbox1 = this.sbox1;
            uint[] sbox2 = this.sbox2;
            uint[] sbox3 = this.sbox3;
            uint[] sbox4 = this.sbox4;

            uint[] pbox = this.pbox;

            uint pbox00 = pbox[ 0];
            uint pbox01 = pbox[ 1];
            uint pbox02 = pbox[ 2];
            uint pbox03 = pbox[ 3];
            uint pbox04 = pbox[ 4];
            uint pbox05 = pbox[ 5];
            uint pbox06 = pbox[ 6];
            uint pbox07 = pbox[ 7];
            uint pbox08 = pbox[ 8];
            uint pbox09 = pbox[ 9];
            uint pbox10 = pbox[10];
            uint pbox11 = pbox[11];
            uint pbox12 = pbox[12];
            uint pbox13 = pbox[13];
            uint pbox14 = pbox[14];
            uint pbox15 = pbox[15];
            uint pbox16 = pbox[16];
            uint pbox17 = pbox[17];

            uint ivHi = this.ivHi;
            uint ivLo = this.ivLo;         

            count &= ~(BLOCK_SIZE - 1);

            end = posIn + count;

            while (posIn < end)
            {
                hi = hiBak = (((uint)dataIn[posIn    ]) << 24) |
                                 (((uint)dataIn[posIn + 1]) << 16) |
                                 (((uint)dataIn[posIn + 2]) <<  8) |
                                         dataIn[posIn + 3];
 
                lo = loBak = (((uint)dataIn[posIn + 4]) << 24) |
                                 (((uint)dataIn[posIn + 5]) << 16) |
                                 (((uint)dataIn[posIn + 6]) <<  8) |
                                         dataIn[posIn + 7];
                posIn += 8; 

                hi ^= pbox17;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox16;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox15;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox14;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox13;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox12;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox11;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox10;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox09;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox08;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox07;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox06;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox05;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox04;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox03;
                lo ^= (((sbox1[(int)(hi >> 24)] + sbox2[(int)((hi >> 16) & 0x0ff)]) ^ sbox3[(int)((hi >> 8) & 0x0ff)]) + sbox4[(int)(hi & 0x0ff)]) ^ pbox02;
                hi ^= (((sbox1[(int)(lo >> 24)] + sbox2[(int)((lo >> 16) & 0x0ff)]) ^ sbox3[(int)((lo >> 8) & 0x0ff)]) + sbox4[(int)(lo & 0x0ff)]) ^ pbox01;

                lo ^= ivHi ^ pbox00;
                hi ^= ivLo;

                dataOut[posOut    ] = (byte)(lo >> 24);
                dataOut[posOut + 1] = (byte)(lo >> 16);
                dataOut[posOut + 2] = (byte)(lo >>  8);
                dataOut[posOut + 3] = (byte) lo;
                
                dataOut[posOut + 4] = (byte)(hi >> 24);
                dataOut[posOut + 5] = (byte)(hi >> 16);
                dataOut[posOut + 6] = (byte)(hi >>  8);
                dataOut[posOut + 7] = (byte) hi;

                ivHi = hiBak;
                ivLo = loBak;

                posOut += 8;
            }

            this.ivHi = ivHi;
            this.ivLo = ivLo;

            return count;
        }

        /// <see cref="BlowfishNET.BlowfishECB.Clone"/>
        public new object Clone()
        {
            BlowfishCBC result;

            result = new BlowfishCBC();

            result.pbox = (uint[]) this.pbox.Clone();
            
            result.sbox1 = (uint[]) this.sbox1.Clone();
            result.sbox2 = (uint[]) this.sbox2.Clone();
            result.sbox3 = (uint[]) this.sbox3.Clone();
            result.sbox4 = (uint[]) this.sbox4.Clone();

            result.block = (byte[]) this.block.Clone();

            result.isWeakKey = this.isWeakKey;
        
            result.ivHi = this.ivHi;    
            result.ivLo = this.ivLo;    

            return result;
        }
    }
}
