using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerState
{
    /// <summary>
    /// M�thode appel�e lors de l'entr�e dans un �tat. Utilis�e pour initialiser l'�tat.
    /// </summary>
    /// <param name="_dataController">R�f�rence au DataController contenant les informations du joueur.</param>
    public void EnterState(ref DataController _dataController);

    /// <summary>
    /// M�thode appel�e pour mettre � jour l'�tat actuel. Contient la logique de l'�tat.
    /// </summary>
    /// <param name="_dataController">R�f�rence au DataController contenant les informations du joueur.</param>
    /// <param name="_dataScriptable">R�f�rence au ScriptableObjectController contenant des donn�es suppl�mentaires pour les calculs.</param>
    public void CurrentStateUpdate(ref DataController _dataController, ScriptableObjectController _dataScriptable);

    /// <summary>
    /// M�thode appel�e lors de la sortie d'un �tat. Utilis�e pour nettoyer ou r�initialiser des donn�es sp�cifiques � l'�tat.
    /// </summary>
    /// <param name="_dataController">R�f�rence au DataController contenant les informations du joueur.</param>
    public void ExitState(ref DataController _dataController);

    /// <summary>
    /// M�thode appel�e pour changer d'�tat en fonction des inputs du joueur.
    /// </summary>
    /// <param name="_dataController">R�f�rence au DataController contenant les informations du joueur.</param>
    public void ChangeStateByInput(ref DataController _dataController);

    /// <summary>
    /// M�thode appel�e pour changer d'�tat en fonction de la physique ou de l'environnement.
    /// </summary>
    /// <param name="_dataController">R�f�rence au DataController contenant les informations du joueur.</param>
    public void ChangeStateByNature(ref DataController _dataController);
}
