using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace AI.Genetics
{
    public class FishSpawner : MonoBehaviour
    {
        public GameObject fishPrefab;

        List<Individual> currentPopulation;

        public void SpawnPopulation(List<Genotype> genotypes)
        {
            currentPopulation = new List<Individual>();

            for (int i = 0; i < genotypes.Count; i++)
            {
                var fishGo = Instantiate(fishPrefab);
                var individual = fishGo.GetComponent<Individual>();
                individual.SetGenotype(genotypes[i]);
                currentPopulation.Add(individual);

                fishGo.transform.position = new Vector3(i * 10, 0, i);
            }
        }

        public void EndSimulation()
        {
            foreach (var individual in currentPopulation)
            {
                // High y = good!
                float fitness = individual.body.transform.position.y;
                individual.genotype.fitness = fitness;

                // Kill the fish
                Destroy(individual.gameObject);
            }
        }
    }
}
