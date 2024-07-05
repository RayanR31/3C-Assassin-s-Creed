using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionState
{
    /// Cette class est le parent des class GS, ils vont servir � v�rifier si oui ou non le controlleur doit changer d'�tat.
    /// Cela �vite d'avoir des states avec des dizaines et dizaines d'�tat.
    /// Elle contient �galement une fonction virtuelle pour g�rer les collisions du contr�leur.
    /// Ainsi, les collisions peuvent �tre modifi�es en fonction de l'�tat si n�cessaire.
    /// <summary>
    /// V�rifie les collisions avec les murs et ajuste la destination en cons�quence.
    /// </summary>
    /// <param name="_dataController">R�f�rence au DataController contenant les informations du joueur.</param>
    /// 
    // La force de l'ajustement de la destination
    private float currentForceCollision;
    // Stocke la normale de la surface en cas de collision, initialis�e vers le bas. Nous la stockons car on ne sait jamais nos raycast perdent leurs cible pendant une frame
    private Vector3 hitNormal = -Vector3.up;
    // Stocke la direction du raycast pour d�tecter les collisions.
    private Vector3 direction;

    public virtual void CheckWall(ref DataController _dataController)
    {
        // Un tableau de directions � v�rifier pour les collisions.
        Vector3[] directionsWorld = {
        -Vector3.forward,
        Vector3.forward,
        Vector3.up,
        -Vector3.up,
        Vector3.right,
        Vector3.left
    };

        // D�finit les tailles pour les SphereCast dans chaque direction
        float[] sizes = { 0.9f, 0.9f, 0.9f, 0.9f, 0.9f, 0.9f };

        // D�finit la force de collision actuelle
        currentForceCollision = 30f; // Peut �tre calcul�e dynamiquement en fonction de la vitesse

        RaycastHit hits;

        // Effectue un SphereCast vers le bas pour d�tecter les collisions avec le sol
        if (Physics.SphereCast(_dataController.destination, 0.5f, -Vector3.up, out hits, 4f, 1 << 0))
        {
            hitNormal = hits.normal; // Met � jour la normale de collision
        }

        // Parcourt chaque direction pour d�tecter les collisions avec les murs
        for (int i = 0; i < directionsWorld.Length; i++)
        {
            RaycastHit hit;

            // Effectue un SphereCast vers le bas pour d�tecter les collisions avec le sol
            if (Physics.SphereCast(_dataController.destination, 0.5f, -Vector3.up, out hit, 4f, 1 << 0))
            {
                hitNormal = hit.normal; // Met � jour la normale de collision
            }

            // Calcule la direction du SphereCast en fonction de la normale de collision
            // On ajoute une s�curit� au cas ou la normal = Vector3.zero car sinon Unity nous harc�le de message !!!
            if (hitNormal != Vector3.zero)
            {
                direction = Quaternion.LookRotation(hitNormal) * directionsWorld[i];
            }
            else
            {
                direction = directionsWorld[i];
            }

            // V�rifie s'il y a une collision dans la direction actuelle
            if (Physics.SphereCast(_dataController.destination, 0.5f, direction, out hit, sizes[i], 1 << 0))
            {
                // Ajuste la destination en fonction de la normale de collision et de la force de collision
                _dataController.destination += (hit.normal * currentForceCollision * Time.fixedDeltaTime);
                // Dessine un rayon rouge pour indiquer une collision
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(hitNormal) * directionsWorld[i] * sizes[i], Color.red);
            }
            else
            {
                // Dessine un rayon vert pour indiquer l'absence de collision
                Debug.DrawRay(_dataController.destination, Quaternion.LookRotation(hitNormal) * directionsWorld[i] * sizes[i], Color.green);
            }

            // V�rifie la collision avec le sol (la deuxi�me direction dans directionsWorld)
            if (directionsWorld[i] == directionsWorld[1])
            {
                // Si une collision est d�tect�e et que la distance est inf�rieure ou �gale � 0.35, arr�te le mouvement
                if (hit.collider != null && hit.distance <= 0.35f)
                {
                    _dataController.direction = Vector3.zero;
                }
            }
        }
    }
}
