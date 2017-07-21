﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Proxy
{
    internal class Chunk
    {
        private string length;
        private ChunkState state;

        public event EventHandler<ChunkCompleteEventArgs> ChunkCompleted;

        public int GetExpectedLength
        {
            get
            {
                return int.Parse(length, System.Globalization.NumberStyles.HexNumber);
            }
        }

        public void ResetLength()
        {
            length = null;
        }

        public Chunk()
        {
            state = new NumberOrCrState(this);
        }

        public void ProcessChunk(byte[] data)
        {
            foreach (var item in data)
            {
                state.Handle(item, ChangeState);
            }
        }

        public void ChangeState(ChunkState state)
        {
            this.state = state;
        }

        public void OnLength(byte data)
        {
            length += (char)data;
        }

        public void OnChunkCompleted(bool isComplete, byte[] data)
        {
            ChunkCompleted?.Invoke(this, new ChunkCompleteEventArgs() { IsComplete = isComplete, Chunk = data });
        }
    }
}