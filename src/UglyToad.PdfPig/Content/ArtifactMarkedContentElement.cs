﻿namespace UglyToad.PdfPig.Content
{
    using Core;
    using Graphics;
    using Tokens;

    /// <inheritdoc />
    /// <summary>
    /// Artifacts are graphics objects that are not part of the author's original content but rather are 
    /// generated by the conforming writer in the course of pagination, layout, or other strictly mechanical 
    /// processes.
    /// <para>Artifacts may also be used to describe areas of the document where the author uses a graphical 
    /// background, with the goal of enhancing the visual experience. In such a case, the background is not 
    /// required for understanding the content. - PDF 32000-1:2008, Section 14.8.2.2</para>
    /// </summary>
    public class ArtifactMarkedContentElement : MarkedContentElement
    {
        /// <summary>
        /// The artifact's type: Pagination, Layout, Page, or (PDF 1.7) Background.
        /// </summary>
        public ArtifactType Type { get; }

        /// <summary>
        /// The artifact's subtype. Standard values are Header, Footer, and Watermark. 
        /// Additional values may be specified for this entry, provided they comply with the naming conventions.
        /// </summary>
        public string? SubType { get; }

        /// <summary>
        /// The artifact's attribute owners.
        /// </summary>
        public string? AttributeOwners { get; }

        /// <summary>
        /// The artifact's bounding box.
        /// </summary>
        public PdfRectangle? BoundingBox { get; }

        /// <summary>
        /// The names of regions this element is attached to.
        /// </summary>
        public IReadOnlyList<NameToken> Attached { get; set; }

        /// <summary>
        /// Is the artifact attached to the top edge?
        /// </summary>
        public bool IsTopAttached => IsAttached(NameToken.Top);

        /// <summary>
        /// Is the artifact attached to the bottom edge?
        /// </summary>
        public bool IsBottomAttached => IsAttached(NameToken.Bottom);

        /// <summary>
        /// Is the artifact attached to the left edge?
        /// </summary>
        public bool IsLeftAttached => IsAttached(NameToken.Left);

        /// <summary>
        /// Is the artifact attached to the right edge?
        /// </summary>
        public bool IsRightAttached => IsAttached(NameToken.Right);

        internal ArtifactMarkedContentElement(int markedContentIdentifier, NameToken tag, DictionaryToken properties,
            string? language,
            string? actualText,
            string? alternateDescription,
            string? expandedForm,
            ArtifactType artifactType,
            string? subType,
            string? attributeOwners,
            PdfRectangle? boundingBox,
            IReadOnlyList<NameToken> attached,
            IReadOnlyList<MarkedContentElement> children,
            IReadOnlyList<Letter> letters,
            IReadOnlyList<PdfPath> paths,
            IReadOnlyList<IPdfImage> images,
            int index) 
            : base(markedContentIdentifier, tag, properties, language,
                actualText,
                alternateDescription,
                expandedForm,
                true,
                children,
                letters,
                paths,
                images,
                index)
        {
            Type = artifactType;
            SubType = subType;
            AttributeOwners = attributeOwners;
            BoundingBox = boundingBox;
            Attached = attached ?? Array.Empty<NameToken>();
        }

        private bool IsAttached(NameToken edge)
        {
            foreach (var name in Attached)
            {
                if (name == edge)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// If present, shall be one of the names Pagination, Layout, Page, or (PDF 1.7) Background.
        /// </summary>
        public enum ArtifactType
        {
            /// <summary>
            /// Unknown artifact type.
            /// </summary>
            Unknown,

            /// <summary>
            /// Ancillary page features such as running heads and folios (page numbers).
            /// </summary>
            Pagination,

            /// <summary>
            /// Purely cosmetic typographical or design elements such as footnote rules or background screens.
            /// </summary>
            Layout,

            /// <summary>
            /// Production aids extraneous to the document itself, such as cut marks and colour bars.
            /// </summary>
            Page,

            /// <summary>
            /// (PDF 1.7) Images, patterns or coloured blocks that either run the entire length and/or 
            /// width of the page or the entire dimensions of a structural element. Background artifacts 
            /// typically serve as a background for content shown either on top of or placed adjacent to 
            /// that background.
            /// <para>A background artifact can further be classified as visual content that serves to enhance the user experience, that lies under the actual content, and that is not required except to retain visual fidelity.</para>
            /// </summary>
            Background
        }
    }
}
