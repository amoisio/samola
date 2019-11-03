namespace Samola.Numbers.CustomTypes
{
    public class NumberPyramidBuilder
    {
        public NumberPyramidBuilder()
        {

        }

        public int[] Data { get; set; }

        public NumberPyramid Build()
        {
            var data = this.Data;
            int len = data.Length;
            PyramidNode[] nodes = new PyramidNode[len];
            int n = 1;
            int n_s, n_e, ab_s, ab_e;
            int? a_i, b_i;

            for (int i = 0; i < len; i++)
            {
                var node = new PyramidNode(data[i]);

                nodes[i] = node;

                // start and end indices of the pyramid nodes on level n
                n_s = (int)(0.5 * n * (n - 1));
                n_e = (int)(0.5 * n * (n + 1) - 1);

                if (i > 0)
                {
                    // start and end indices of the pyramid nodes on level n - 1
                    ab_s = (int)(0.5 * (n - 1) * (n - 2));
                    ab_e = n_s - 1;

                    // determine parent item indices a_i and b_i on level n - 1
                    if (i == n_s)
                    { 
                        // current item is at the start of the level => left parent is null
                        b_i = ab_s;
                        a_i = null;
                        
                    }
                    else if (i == n_e)
                    {
                        // current item is at the end of the level => right parent is null
                        b_i = null;
                        a_i = ab_e;
                    }
                    else
                    {
                        int offset = i - n_s;
                        b_i = ab_s + offset;
                        a_i = b_i - 1;
                    }

                    if (a_i.HasValue) nodes[a_i.Value].Right = node;
                    if (b_i.HasValue) nodes[b_i.Value].Left = node;
                }

                // If at the end index of the level, increment level for the next iteration
                if (i == n_e && i != len - 1) n++;
            }
            return new NumberPyramid(nodes);
        }
    }

    
}
