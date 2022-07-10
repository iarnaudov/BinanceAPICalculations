namespace Forex.Services.Helpers
{
    public class FixedSizeQueue
    {
        private readonly Queue<decimal> queue = new Queue<decimal>();
        public readonly int Size;

        public FixedSizeQueue(int size)
        {
            if (size <= 0)
            {
                throw new ArgumentOutOfRangeException(nameof(size), "Must be greater than 0");
            }
            this.Size = size;
        }

        public void Enqueue(decimal obj)
        {
            queue.Enqueue(obj);

            while (queue.Count > this.Size)
            {
                decimal outObj;
                queue.TryDequeue(out outObj);
            }
        }

        public decimal Sum()
        {
            decimal sum = 0;
            foreach (var item in this.queue)
            {
                sum += item;
            }
            return sum;
        }
    }
}
