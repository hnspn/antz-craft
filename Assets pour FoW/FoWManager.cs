using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FoWManager : MonoBehaviour {
    #region Private
    /// <summary>
    /// The size of the texture in BOTH x and y.
    /// Should be a power of 2.
    /// </summary>
    [SerializeField]
    private int _textureSize = 256;
    [SerializeField]
    private Color _fogOfWarColor;
    [SerializeField]
    private LayerMask _fogOfWarLayer;

    private Texture2D _texture;
    private Color[] _pixels;
    private List<FoWRevealer> _revealers;
    private int _pixelsPerUnit;
    private Vector2 _centerPixel;

    private int frameCount = 0;

    private static FoWManager _instance;
    #endregion

    #region Public
    /// <summary>
    /// Note this is NOT a singleton!
    /// This just needs to be globally accessable AND still be a MonoBehaviour.
    /// </summary>

    public int framesBetweenRedraw = 10;
    public static FoWManager Instance {
        get {
            return _instance;
        }
    }

    public int TextureSize {
        get {
            return _textureSize;
        }

        set {
            _textureSize = value;
        }
    }

    public Color FogOfWarColor {
        get {
            return _fogOfWarColor;
        }

        set {
            _fogOfWarColor = value;
        }
    }

    public LayerMask FogOfWarLayer {
        get {
            return _fogOfWarLayer;
        }

        set {
            _fogOfWarLayer = value;
        }
    }

    public Texture2D Texture {
        get {
            return _texture;
        }

        set {
            _texture = value;
        }
    }

    public Color[] Pixels {
        get {
            return _pixels;
        }

        set {
            _pixels = value;
        }
    }

    public List<FoWRevealer> Revealers {
        get {
            return _revealers;
        }

        set {
            _revealers = value;
        }
    }

    public int PixelsPerUnit {
        get {
            return _pixelsPerUnit;
        }

        set {
            _pixelsPerUnit = value;
        }
    }

    public Vector2 CenterPixel {
        get {
            return _centerPixel;
        }

        set {
            _centerPixel = value;
        }
    }

    public static FoWManager Instance1 {
        get {
            return _instance;
        }

        set {
            _instance = value;
        }
    }
    #endregion

    private void Awake() {
        _instance = this;


        var renderer = GetComponent<Renderer>();
        Material fogOfWarMat = null;
        if (renderer != null) {
            fogOfWarMat = renderer.material;
        }

        if (fogOfWarMat == null) {
            UnityEngine.Debug.LogError("Material for Fog Of War not found!");
            return;
        }

        _texture = new Texture2D(_textureSize, _textureSize, TextureFormat.RGBA32, false);
        _texture.wrapMode = TextureWrapMode.Clamp;

        _pixels = _texture.GetPixels();
        ClearPixels();

        fogOfWarMat.mainTexture = _texture;

        _revealers = new List<FoWRevealer>();
        _pixelsPerUnit = Mathf.RoundToInt(_textureSize / transform.lossyScale.x);

        _centerPixel = new Vector2(_textureSize * 0.5f, _textureSize * 0.5f);
    }

    public void RegisterRevealer(FoWRevealer revealer) {
        _revealers.Add(revealer);
    }

    private void ClearPixels() {
        for (var i = 0; i < _pixels.Length; i++) {
            _pixels[i] = _fogOfWarColor;
        }
    }

    /// <summary>
    /// Sets the pixels in _pixels to clear a circle.
    /// </summary>
    /// <param name="originX">in pixels</param>
    /// <param name="originY">in pixels</param>
    /// <param name="radius">in unity units</param>
    private void CreateCircle(int originX, int originY, int radius) {
        for (var y = -radius * _pixelsPerUnit; y <= radius * _pixelsPerUnit; ++y) {
            for (var x = -radius * _pixelsPerUnit; x <= radius * _pixelsPerUnit; ++x) {
                if (x * x + y * y <= (radius * _pixelsPerUnit) * (radius * _pixelsPerUnit)) {
                    if((originY + y >= 0 && originY + y < _textureSize) && (originX + x >= 0 && originX + x < _textureSize)) {
                        _pixels[(originY + y) * _textureSize + originX + x] = new Color(0, 0, 0, 0);
                    }
                }
            }
        }
    }

    // Update is called once per frame
    private void Update() {
        frameCount++;
        if (frameCount == framesBetweenRedraw) {
            frameCount = 0;
            ClearPixels();

            foreach (FoWRevealer revealer in _revealers) {
                // should do a raycast from the revealer to the camera.
                var screenPoint = Camera.main.WorldToScreenPoint(revealer.transform.position);
                var ray = Camera.main.ScreenPointToRay(screenPoint);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit, 1000, _fogOfWarLayer.value)) {
                    // Translates the revealer to the center of the fog of war.
                    // This way the position lines up with the center pixel and can be converted easier.
                    var translatedPos = hit.point - transform.position;

                    var pixelPosX = Mathf.RoundToInt(translatedPos.x * _pixelsPerUnit + _centerPixel.x);
                    var pixelPosY = Mathf.RoundToInt(translatedPos.y * _pixelsPerUnit + _centerPixel.y);

                    CreateCircle(pixelPosX, pixelPosY, revealer.radius);
                }
            }
            _texture.SetPixels(_pixels);
            _texture.Apply(false); 
        }
    }
}
