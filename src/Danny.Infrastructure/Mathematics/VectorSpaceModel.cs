using System;
using System.Collections.Generic;

namespace Danny.Infrastructure.Mathematics
{
    /// <summary>
    /// 空间向量模型
    /// </summary>
    class VectorSpaceModel
    {
        public VectorSpaceModel()
        {

        }

        /// <summary>
        /// 相似度计算
        /// </summary>
        /// <param name="dict1">第一篇文档的词频词典</param>
        /// <param name="dict2">第二篇文档的词频词典</param>
        /// <returns>两篇文档的相似度</returns>
        public double CalSimilarity(Dictionary<string, int> dict1, Dictionary<string, int> dict2)
        {
            if (dict1 == null)
            {
                throw new ArgumentNullException("dict1");
            }
            if (dict2 == null)
            {
                throw new ArgumentNullException("dict2");
            }

            var dictionary1 = new Dictionary<string, int>(dict1);
            var dictionary2 = new Dictionary<string, int>(dict2);
            if ((dictionary1.Count < 1) || (dictionary2.Count < 1))
            {
                return 0.0;
            }

            double similarity = 0.0,
                   numerator = 0.0,
                   denominator1 = 0.0,
                   denominator2 = 0.0;
            int temp1, temp2;
            Dictionary<string, int>.KeyCollection keys1 = dictionary1.Keys;
            Dictionary<string, int>.KeyCollection keys2 = dictionary2.Keys;

            foreach (string key in keys1)
            {
                dictionary1.TryGetValue(key, out temp1);
                if (!dictionary2.TryGetValue(key, out temp2))
                {
                    temp2 = 0;
                }
                dictionary2.Remove(key);
                numerator += temp1 * temp2;
                denominator1 += temp1 * temp1;
                denominator2 += temp2 * temp2;
            }

            foreach (string key in keys2)
            {
                dictionary2.TryGetValue(key, out temp2);
                denominator2 += temp2 * temp2;
            }

            similarity = numerator / (Math.Sqrt(denominator1 * denominator2));
            return similarity;
        }
    }
}
