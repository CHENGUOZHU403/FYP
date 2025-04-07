using UnityEngine;

[CreateAssetMenu(fileName = "NewMathPuzzleData", menuName = "Puzzle System/Math Puzzle Data")]
public class MathPuzzleData : ScriptableObject
{
    [System.Serializable]
    public class MathProblem
    {
        public string problemText;
        public int correctAnswer;
        public int difficultyLevel;
    }

    public MathProblem[] additionProblems;
    public MathProblem[] subtractionProblems;
    public MathProblem[] multiplicationProblems;
    public MathProblem[] divisionProblems;

    public MathProblem GetRandomProblem()
    {
        // Choose random type problem
        int problemType = Random.Range(0, 4);

        // 根據類型返回隨機問題
        switch (problemType)
        {
            case 0: return additionProblems[Random.Range(0, additionProblems.Length)];
            case 1: return subtractionProblems[Random.Range(0, subtractionProblems.Length)];
            case 2: return multiplicationProblems[Random.Range(0, multiplicationProblems.Length)];
            case 3: return divisionProblems[Random.Range(0, divisionProblems.Length)];
            default: return additionProblems[0];
        }
    }

    public MathProblem GetProblemByDifficulty(int targetDifficulty)
    {
        // 收集所有符合難度的問題
        System.Collections.Generic.List<MathProblem> suitableProblems = new System.Collections.Generic.List<MathProblem>();

        foreach (var problem in additionProblems)
            if (problem.difficultyLevel == targetDifficulty) suitableProblems.Add(problem);

        foreach (var problem in subtractionProblems)
            if (problem.difficultyLevel == targetDifficulty) suitableProblems.Add(problem);

        foreach (var problem in multiplicationProblems)
            if (problem.difficultyLevel == targetDifficulty) suitableProblems.Add(problem);

        foreach (var problem in divisionProblems)
            if (problem.difficultyLevel == targetDifficulty) suitableProblems.Add(problem);

        // 如果沒有找到完全匹配的難度，找最接近的
        if (suitableProblems.Count == 0)
        {
            int closestDiff = int.MaxValue;
            MathProblem closestProblem = additionProblems[0];

            // 檢查所有問題尋找最接近的難度
            var allProblems = new System.Collections.Generic.List<MathProblem>();
            allProblems.AddRange(additionProblems);
            allProblems.AddRange(subtractionProblems);
            allProblems.AddRange(multiplicationProblems);
            allProblems.AddRange(divisionProblems);

            foreach (var problem in allProblems)
            {
                int diff = Mathf.Abs(problem.difficultyLevel - targetDifficulty);
                if (diff < closestDiff)
                {
                    closestDiff = diff;
                    closestProblem = problem;
                }
            }
            return closestProblem;
        }

        return suitableProblems[Random.Range(0, suitableProblems.Count)];
    }
}