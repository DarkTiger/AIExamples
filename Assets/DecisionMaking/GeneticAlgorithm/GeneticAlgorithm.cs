using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.Genetics
{
    public class GeneticAlgorithm : MonoBehaviour
    {
        public FishSpawner fishSpawner;
        public int populationSize = 100;
        public int nGenerations = 1000;

        void Start()
        {
            // Create initial populations
            List<Genotype> initialGenotypes = new List<Genotype>();
            for (int i = 0; i < populationSize; i++)
            {
                Genotype g = new Genotype();
                g.bodySizeX = Random.Range(1, 10);
                g.bodySizeY = Random.Range(1, 10);
                g.finSizeX = Random.Range(1, 10);
                g.finSizeY = Random.Range(1, 10);
                g.motorForce = Random.Range(0, 5000);
                initialGenotypes.Add(g);
            }

            StartCoroutine(GeneticCO(initialGenotypes));
        }

        IEnumerator GeneticCO(List<Genotype> initialGenotypes)
        {
            List<Genotype> currentGenotypes = initialGenotypes;

            // Do N generation
            for (int i = 0; i < nGenerations; i++)
            {
                // Spawn fish
                fishSpawner.SpawnPopulation(currentGenotypes);

                // ... wait for simulation
                // WAIT
                yield return new WaitForSeconds(3.0f);

                fishSpawner.EndSimulation();

                // Selection
                currentGenotypes.Sort((x, y) => (int)(100*y.fitness - 100*x.fitness));

                // 30 fish
                List<Genotype> selectedGenotypes = currentGenotypes.GetRange(0, (int)(0.3f * populationSize));
                List<Genotype> crossoverGenotypes = new List<Genotype>();
                for (int ci = 0; ci < selectedGenotypes.Count/2; ci++)
                {
                    Genotype p1 = selectedGenotypes[ci * 2];
                    Genotype p2 = selectedGenotypes[ci * 2 + 1];

                    Genotype c1 = new Genotype();
                    c1.bodySizeX = Random.value > 0.5f ? p1.bodySizeX : p2.bodySizeX;
                    c1.bodySizeY = Random.value > 0.5f ? p1.bodySizeY : p2.bodySizeY;
                    c1.finSizeX = Random.value > 0.5f ? p1.finSizeX : p2.finSizeX;
                    c1.finSizeY = Random.value > 0.5f ? p1.finSizeY : p2.finSizeY;
                    c1.motorForce = Random.value > 0.5f ? p1.motorForce : p2.motorForce;

                    Genotype c2 = new Genotype();
                    c2.bodySizeX = Random.value > 0.5f ? p1.bodySizeX : p2.bodySizeX;
                    c2.bodySizeY = Random.value > 0.5f ? p1.bodySizeY : p2.bodySizeY;
                    c2.finSizeX = Random.value > 0.5f ? p1.finSizeX : p2.finSizeX;
                    c2.finSizeY = Random.value > 0.5f ? p1.finSizeY : p2.finSizeY;
                    c2.motorForce = Random.value > 0.5f ? p1.motorForce : p2.motorForce;

                    crossoverGenotypes.Add(c1);
                    crossoverGenotypes.Add(c2);
                }

                // Mutation
                List<Genotype> mutatedGenotypes = new List<Genotype>();
                int nMutated = populationSize - selectedGenotypes.Count - crossoverGenotypes.Count;
                for (int mi = 0; mi < nMutated; mi++)
                {
                    Genotype p = crossoverGenotypes[Random.Range(0, crossoverGenotypes.Count)];
                    Genotype m = new Genotype();
                    m.bodySizeX = p.bodySizeX;
                    m.bodySizeY = p.bodySizeY;
                    m.finSizeX = p.finSizeX;
                    m.finSizeY = p.finSizeY;
                    m.motorForce = p.motorForce;

                    int choice = Random.Range(0, 5);
                    switch (choice)
                    {
                        case 0: m.bodySizeX = Random.Range(1, 40); break;
                        case 1: m.bodySizeY = Random.Range(1, 40); break;
                        case 2: m.finSizeX = Random.Range(1, 40); break;
                        case 3: m.finSizeY = Random.Range(1, 40); break;
                        case 4: m.motorForce = Random.Range(-10000, 10000); break;
                    }

                    mutatedGenotypes.Add(m);

                    currentGenotypes = new List<Genotype>();
                    currentGenotypes.AddRange(selectedGenotypes);
                    currentGenotypes.AddRange(crossoverGenotypes);
                    currentGenotypes.AddRange(mutatedGenotypes);

                    Debug.Log("Generation " + i + " best fitness: " + selectedGenotypes[0].fitness);
                }
            }
        }
    }
}
