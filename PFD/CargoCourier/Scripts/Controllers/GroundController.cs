using System.Collections.Generic;
using UnityEngine;

public class GroundController : GameElement
{
    public List<Texture2D> textures;
    private List<Texture2D> _textures = new List<Texture2D>();
    private Texture2D _current;

    private void Start()
    {
        AddTextures();
        ChangeTexture();
    }

    void AddTextures()
    {
        foreach (Texture2D _texture2D in textures)
        {
            _textures.Add(_texture2D);
        }
        for (int i = 0; i < _textures.Count; i++)
        {
            Texture2D temp = _textures[i];
            int randomIndex = Random.Range(i, _textures.Count);
            _textures[i] = _textures[randomIndex];
            _textures[randomIndex] = temp;
        }
    }

    public void ChangeTexture()
    {
        GetComponent<Renderer>().material.mainTexture = Texture();
    }

    private Texture2D Texture()
    {
        foreach (Texture2D _texture2D in _textures)
        {
            if (_texture2D != _current)
            {
                _current = _texture2D;
                _textures.Remove(_texture2D);
                if (_textures.Count <= 0)
                {
                    AddTextures();
                }
                break;
            }
        }
        return _current;
    }
}
